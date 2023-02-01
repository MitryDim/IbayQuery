using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class PayementsEntities
    {
        private readonly List<string> _validStatuses = new List<string> { "pending", "success", "failed" };
        private string _status;

       [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }

        public string Status {
            get
            {
                return _status;
            }
            set {
                if (!_validStatuses.Contains(value))
                    throw new ArgumentException("Le status saisie n'est pas reconnu ! ");
                _status = value;
            } 
        }

        public DateTime CreatedAt { get; set; }

        public virtual OrdersEntities Order { get; set; }

       
    }
}
