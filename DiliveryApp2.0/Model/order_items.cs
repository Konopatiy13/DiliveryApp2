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
    
    public partial class order_items
    {
        public int order_item_id { get; set; }
        public int order_id { get; set; }
        public int menu_item_id { get; set; }
        public int quantity { get; set; }
    
        public virtual menu_items menu_items { get; set; }
        public virtual orders orders { get; set; }
    }
}
