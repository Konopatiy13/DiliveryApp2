//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiliveryApp2._0.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class couriers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public couriers()
        {
            this.deliveries = new HashSet<deliveries>();
        }
    
        public int courier_id { get; set; }
        public string courier_name { get; set; }
        public string courier_phone { get; set; }
        public string vehicle_type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<deliveries> deliveries { get; set; }
    }
}
