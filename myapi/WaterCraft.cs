//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myapi
{
    using System;
    using System.Collections.Generic;
    
    public partial class WaterCraft
    {
        public WaterCraft()
        {
            this.RentJournal = new HashSet<RentJournal>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int WatercraftTypeId { get; set; }
        public int WatercraftConditionId { get; set; }
        public decimal CostRate { get; set; }
    
        public virtual WatercraftType WatercraftType { get; set; }
        public virtual WatercraftCondition WatercraftCondition { get; set; }
        public virtual ICollection<RentJournal> RentJournal { protected get; set; }
    }
}
