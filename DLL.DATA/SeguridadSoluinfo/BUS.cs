
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DLL.DATA.SeguridadSoluinfo
{

using System;
    using System.Collections.Generic;
    
public partial class BUS
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public BUS()
    {

        this.SERVICIO_REALIZADO = new HashSet<SERVICIO_REALIZADO>();

    }


    public int ID_BUS { get; set; }

    public int ID_TERMINAL { get; set; }

    public int ID_INTERNO_BUS { get; set; }

    public string PPU { get; set; }

    public bool ESTADO { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<SERVICIO_REALIZADO> SERVICIO_REALIZADO { get; set; }

}

}
