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
    
    public partial class PayMethod
    {
        public PayMethod()
        {
            this.RentJournal = new HashSet<RentJournal>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<RentJournal> RentJournal { protected get; set; }
    }
}