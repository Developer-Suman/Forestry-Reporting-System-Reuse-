using DevDefined.OAuth.Framework;
using Reuse.Bll.DTO;
using Reuse.Bll.Repository.Interface;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class BranchServices : IBranchServices<BranchDTO>
    {
        private readonly IRepository<Branch> _repository;
        private readonly ITokenServices _tokenServices;
        private readonly IRepository<BranchType> _branchType;
        private readonly ISeedServices _seedServices;

        public BranchServices(IRepository<Branch> repository, ITokenServices tokenServices, IRepository<BranchType> branchType, ISeedServices seedServices)
        {
            _repository = repository;
            _tokenServices = tokenServices;
            _branchType = branchType;
            _seedServices = seedServices;

        }
        public async Task<ServiceResult<BranchDTO>> AddBranch(BranchDTO entity, string? authorization)
        {
            try
            {
                //get token from authorization for user roles
                if (authorization != null)
                {
                    var token = authorization.Split(" ")[1];
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);

                    var chechRole = jwt.Claims.FirstOrDefault(x => x.Type == "role");
                    var chechBranch = jwt.Claims.FirstOrDefault(x => x.Type == "bid");

                    if (chechRole != null && chechBranch != null)
                    {
                        if (chechRole.Value == "Admin")
                        {
                            entity.ParentBranchId = int.Parse(chechBranch.Value);
                            entity.BranchTypeId = 3;
                        }

                    }

                }
                var parentBranch = await _repository.CheckIdIfExists(x => x.BranchId == entity.ParentBranchId);
                var branchType = await _branchType.CheckIdIfExists(x => x.BranchTypeId == entity.BranchTypeId);

                if (!parentBranch || !branchType)
                {
                    var errors = new List<string>();
                    if (!parentBranch) errors.Add("मुख्य शाखा फेला परेन");
                    if (!branchType) errors.Add("शाखाको प्रकार फेला परेन");
                    return new ServiceResult<BranchDTO>(false, errors: errors.ToArray());
                }


                entity.IsActive = true;
                var branch = await _repository.AddAsync(entity.ToBranch(entity));
                await _seedServices.SeedUser(branch.BranchId);
                return new ServiceResult<BranchDTO>(true, new BranchDTO().ToBranchDTO(branch));

            }
            catch (Exception ex)
            {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<BranchDTO>> AddInitialBranch(BranchDTO entity)
        {
            try
            {
                var checkBranch = await _repository.AnyAsync();
                if(!checkBranch)
                {
                    entity.BranchTypeId = 1;
                    entity.IsActive = true;
                    var branch = await _repository.AddAsync(entity.ToBranch(entity));
                    if (branch.BranchId != null)
                    {
                        await _seedServices.SeedUser(branch.BranchId);
                    }
                    return new ServiceResult<BranchDTO>(true);
                }
                return new ServiceResult<BranchDTO>(false, errors: new[] { "माफ गर्नुहोस्, त्यहाँ पहिले नै मुख्य शाखा छ" });
                

            }
            catch(Exception ex)
            {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<BranchDTO>> CheckInitialization()
        {
            try
            {
                if(await _repository.AnyAsync())
                {
                    return new ServiceResult<BranchDTO>(true);
                }
                return new ServiceResult<BranchDTO>(false);

            }
            catch(Exception ex)
            {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
            
        }

        public async Task<ServiceResult<BranchDTO>> DeleteBranch(int Id)
        {
            try
            {
                var branchExists = await _repository.GetByIdAsync(Id);
                if(branchExists != null)
                {
                    await _repository.DeleteAsync(branchExists);
                    return new ServiceResult<BranchDTO>(true, new BranchDTO().ToBranchDTO(branchExists));
                }
                return new ServiceResult<BranchDTO>(false, errors: new string[] { "Item not found" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<List<BranchDTO>>> GetAllBranch()
        {
            try
            {
                var getRoles = _tokenServices.GetUsername();
                if(getRoles !=null)
                {
                    if(getRoles == "Admin")
                    {
                        var branches = await _repository.WhereAsync(x=>x.ParentBranchId == _tokenServices.GetBranchId());
                        List<BranchDTO> branchDTOs1 = new();

                        if(branches.Count()>0)
                        {
                            for(int i=0; i<branches.Count(); i++)
                            {
                                branchDTOs1.Add(new BranchDTO()
                                {
                                    BranchId = branches[i].BranchId,
                                    BranchName = branches[i].BranchName,
                                    BranchCode = branches[i].BranchCode,
                                    BranchAddress = branches[i].BranchAddress,
                                    ParentBranchId = branches[i].ParentBranchId,
                                    ParentBranch = (branches[i].ParentBranchId.HasValue) ? (await _repository.GetByIdAsync((int)branches[i].ParentBranchId))?.BranchName : null,
                                    PhoneNo = branches[i].PhoneNo,
                                    Email  = branches[i].Email,
                                    IsActive = branches[i].IsActive,
                                    BranchTypeId = branches[i].BranchTypeId,
                                    BranchType = (await _branchType.GetByIdAsync(branches[i].BranchTypeId))?.BranchTypeNameInNepali?? null

                                });
                            }

                            return new ServiceResult<List<BranchDTO>>(true, branchDTOs1);
                        }

                        return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "शाखाहरू फेला परेनन्" });
                    }
                    else
                    {
                        var branches = await _repository.GetAllAsync();
                        List<BranchDTO> branchDTOs = new();
                        if(branches.Count() > 0)
                        {
                            for(int i=0; i<branches.Count(); i++)
                            {
                                branchDTOs.Add(new BranchDTO()
                                {
                                    BranchId = branches[i].BranchId,
                                    BranchName = branches[i].BranchName,
                                    BranchAddress = branches[i].BranchAddress,
                                    BranchCode = branches[i].BranchCode,
                                    ParentBranchId = branches[i].ParentBranchId,
                                    ParentBranch = (branches[i].ParentBranchId.HasValue) ?( await _repository.GetByIdAsync((int)branches[i].ParentBranchId))?.BranchName : null,
                                    PhoneNo = branches[i].PhoneNo,
                                    Email = branches[i].Email,
                                    IsActive = branches[i].IsActive,
                                    BranchTypeId = branches[i].BranchTypeId,
                                    BranchType = (await _branchType.GetByIdAsync(branches[i].BranchTypeId))?.BranchTypeNameInNepali ?? null
                            });
                            }
                            return new ServiceResult<List<BranchDTO>>(true, branchDTOs);

                        }

                        return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "शाखाहरू फेला परेनन्" });
                    }
                }
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "Unauthorized" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<BranchDTO>> GetBranchById(int Id)
        {
            try
            {
                var branch = await _repository.GetByIdAsync(Id);
                if(branch != null)
                {
                    BranchDTO branchDTO = new BranchDTO()
                    {
                        BranchId = branch.BranchId,
                        BranchName = branch.BranchName,
                        BranchAddress = branch.BranchAddress,
                        BranchCode = branch.BranchCode,
                        ParentBranchId = branch.ParentBranchId,
                        ParentBranch = (branch.ParentBranchId.HasValue) ? (await _repository.GetByIdAsync((int)branch.ParentBranchId))?.BranchName : null,
                        PhoneNo = branch.PhoneNo,
                        Email = branch.Email,
                        IsActive = branch.IsActive,
                        BranchTypeId = branch.BranchTypeId,
                        BranchType = (await _branchType.GetByIdAsync(branch.BranchTypeId))?.BranchTypeNameInNepali ?? null
                    };

                    return new ServiceResult<BranchDTO>(true, branchDTO);
                }

                return new ServiceResult<BranchDTO>(false, errors: new string[] { "शाखा फेला परेनन्" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
        }

        public Task<ServiceResult<List<BranchDTO>>> GetBranchesByBranchType(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<BranchDTO>>> GetBranchesById(int Id)
        {
            try
            {
                var branches = await _repository.WhereAsync(x=>(x.BranchId == Id || x.ParentBranchId == Id) && x.IsActive == true);
                List<BranchDTO> branchDTOs = new();
                if(branches.Count > 0)
                {
                    for(int i=0; i<branches.Count(); i++)
                    {
                        branchDTOs.Add(new BranchDTO()
                        {
                            BranchId= branches[i].BranchId,
                            BranchName = branches[i].BranchName,
                            BranchCode = branches[i].BranchCode,
                            BranchAddress = branches[i].BranchAddress,
                            ParentBranch = (branches[i].ParentBranchId.HasValue)? (await _repository.GetByIdAsync((int)branches[i].ParentBranchId))? .BranchName: null,
                            ParentBranchId = branches[i].ParentBranchId,
                            PhoneNo = branches[i].PhoneNo,
                            Email= branches[i].Email,
                            IsActive = branches[i].IsActive,
                            BranchTypeId = branches[i].BranchTypeId,
                            BranchType = (await _branchType.GetByIdAsync(branches[i].BranchTypeId))?.BranchTypeNameInNepali ?? null

                        });

                    }
                    return new ServiceResult<List<BranchDTO>>(true, branchDTOs);
                }
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "शाखाहरू फेला परेनन्" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<BranchDTO>>> GetBranchesToCreateAdmin()
        {
            try
            {
                var branches = await _repository.WhereAsync(x => x.IsActive == true && x.BranchTypeId == 2);
                List<BranchDTO> branchDTOs = new();
                if(branches.Count()>0)
                {
                    for(int i=0; i<branches.Count(); i++)
                    {
                        branchDTOs.Add(new BranchDTO()
                        {
                            BranchId = branches[i].BranchId,
                            BranchName = branches[i].BranchName,
                            BranchCode = branches[i].BranchCode,
                            BranchAddress = branches[i].BranchAddress,
                            ParentBranchId = branches[i].ParentBranchId,
                            ParentBranch = (branches[i].ParentBranchId.HasValue)?( await _repository.GetByIdAsync((int)branches[i].ParentBranchId))?.BranchName: null,
                            PhoneNo = branches[i].PhoneNo,
                            Email = branches[i].Email,
                            IsActive = branches[i].IsActive,
                            BranchTypeId = branches[i].BranchTypeId,
                            BranchType = (await _branchType.GetByIdAsync(branches[i].BranchTypeId))? .BranchTypeNameInNepali ?? null


                        });
                    }
                    return new ServiceResult<List<BranchDTO>>(true, branchDTOs);
                }
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "शाखाहरू फेला परेनन्" });
            }
            catch(Exception ex)
            {
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<List<BranchDTO>>> GetHeadBranches(int ParentBranchId)
        {
            try
            {
                var branches = await _repository.WhereAsync(x => x.ParentBranchId == ParentBranchId && x.IsActive == true);
                List<BranchDTO> branchDTOs = new();
                if(branches.Count()>0)
                {
                    for(int i=0; i<branches.Count(); i++)
                    {
                        branchDTOs.Add(new BranchDTO()
                        {
                            BranchId = branches[i].BranchId,
                            BranchName= branches[i].BranchName,
                            BranchCode = branches[i].BranchCode,
                            BranchAddress = branches[i].BranchAddress,
                            ParentBranchId = branches[i].ParentBranchId,
                            ParentBranch = (branches[i].ParentBranchId.HasValue)?(await _repository.GetByIdAsync((int)branches[i].ParentBranchId))? .BranchName: null,
                            PhoneNo = branches[i].PhoneNo,
                            Email = branches[i].Email,
                            IsActive = branches[i].IsActive,
                            BranchTypeId = branches[i].BranchTypeId,
                            BranchType = (await _branchType.GetByIdAsync(branches[i].BranchTypeId))? .BranchTypeNameInNepali ?? null
                        });
                    }
                    return new ServiceResult<List<BranchDTO>>(true, branchDTOs);
                }

                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { "शाखा फेला परेनन्" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<BranchDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<BranchDTO>> UpdateBranch( BranchDTO entity)
        {
            try
            {
                var checkBranch = await _repository.GetByIdAsync(entity.BranchId);
                if(checkBranch == null)
                {
                    return new ServiceResult<BranchDTO>(false, errors: new string[] { "खराब अनुरोध" });
                }

                //Checking for the foreign key
                var parentBranch = await _repository.CheckIdIfExists(x=>x.BranchId == entity.ParentBranchId);
                var branchType = await _branchType.CheckIdIfExists(x => x.BranchTypeId == entity.BranchTypeId);

                if(!parentBranch || !branchType)
                {
                    var errors = new List<string>();
                    if (!parentBranch) errors.Add("मुख्य शाखा फेला परेन");
                    if (!branchType) errors.Add("शाखाको प्रकार फेला परेन");
                    return new ServiceResult<BranchDTO>(false, errors: errors.ToArray());
                }

                //Making Child branch Inactive If parent Branch is Active and viceversa
                if(entity.IsActive == false || entity.IsActive == true)
                {
                    var getChildBranch = await _repository.WhereAsync(x=>x.ParentBranchId == entity.BranchId);
                    if(getChildBranch.Count()>0)
                    {
                        for(int i=0; i<getChildBranch.Count(); i++)
                        {
                            getChildBranch[i].IsActive = entity.IsActive;
                        }
                    }
                }

                var branch = await _repository.UpdateAsync(new BranchDTO().ToUpdatedBranch(checkBranch, entity));
                return new ServiceResult<BranchDTO>(true, new BranchDTO().ToBranchDTO(branch));

            }
            catch(Exception ex) {
                return new ServiceResult<BranchDTO>(false, errors: new[] { ex.Message });
            }
        }
    }
}
