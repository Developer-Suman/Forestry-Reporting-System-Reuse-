using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class TaxRateDTO
    {
        public int TaxRateId { get; set; }
        public string TaxRateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }



        public TaxRateDTO ToTaxRateDTO(TaxRate taxRate)
        {
            TaxRateDTO taxRateDTO = new()
            {
                TaxRateId = taxRate.TaxRateId,
                TaxRateName = taxRate.TaxRateName,
                IsActive = taxRate.IsActive
            };
            return taxRateDTO;
        }


        public TaxRate ToTaxRate(TaxRateDTO taxRateDTO)
        {
            TaxRate taxRate = new()
            {
                TaxRateId = taxRateDTO.TaxRateId,
                TaxRateName = taxRateDTO.TaxRateName,
                IsActive = taxRateDTO.IsActive
            };

            return taxRate;

        }


        public List<TaxRateDTO> ToTaxRateDTOList(List<TaxRate> taxRates) 
        {
            List<TaxRateDTO> taxRateDTOs = new();
            for(int i = 0; i < taxRates.Count(); i++)
            {

                taxRateDTOs.Add(new TaxRateDTO().ToTaxRateDTO(taxRates[i]));
            }

            return taxRateDTOs;
        }
    }
}
