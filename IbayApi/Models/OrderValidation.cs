using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Mvc;

namespace IbayApi.Models
{

    /// <summary>
    /// Validation 
    /// </summary>
    public class OrderInput
    {
        /// <summary>
        /// Status prenant pending,success ou failed
        /// </summary>
        [Required]
        public string status { get; set; }

        /// <summary>
        /// Type de paymenent Exemple : Paypal,Carte de crédit,Virement bancaire...
        /// </summary>
        [Required]
        public string paymentType { get; set; }

        /// <summary>
        /// Montant total à payer
        /// </summary>
        [Required]
        public decimal TotalPrice { get; set; }

    }
}
