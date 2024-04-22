using System.ComponentModel.DataAnnotations;

namespace TechBazaar.Domain.Enum
{
    public enum OrderStatusEnum
    {
        [Display(Name = "Не оплачено")]
        NotPayed = 0,
        [Display(Name = "Оплачено")]
        Payed = 1,
        [Display(Name = "Доставлено")]
        Dilivered = 2
    }
}