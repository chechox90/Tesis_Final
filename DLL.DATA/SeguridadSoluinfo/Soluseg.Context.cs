﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class SolusegEntities : DbContext
{
    public SolusegEntities()
        : base("name=SolusegEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<ACCIONES> ACCIONES { get; set; }

    public virtual DbSet<AREAS> AREAS { get; set; }

    public virtual DbSet<MENU> MENU { get; set; }

    public virtual DbSet<PERFILES> PERFILES { get; set; }

    public virtual DbSet<PLATAFORMAS> PLATAFORMAS { get; set; }

    public virtual DbSet<PROYECTO_AGRUPACION> PROYECTO_AGRUPACION { get; set; }

    public virtual DbSet<PROYECTOS> PROYECTOS { get; set; }

    public virtual DbSet<VERSION_PROYECTO> VERSION_PROYECTO { get; set; }

    public virtual DbSet<PERMISOS_ESPECIALES> PERMISOS_ESPECIALES { get; set; }

    public virtual DbSet<NOVEDADES> NOVEDADES { get; set; }

    public virtual DbSet<TIPOS_NOVEDADES> TIPOS_NOVEDADES { get; set; }

    public virtual DbSet<BITACORA_MOVIMIENTOS> BITACORA_MOVIMIENTOS { get; set; }

    public virtual DbSet<TIPO_MOVIEMIENTO_HORARIO> TIPO_MOVIEMIENTO_HORARIO { get; set; }

    public virtual DbSet<BUS> BUS { get; set; }

    public virtual DbSet<CARGA_HORARIO> CARGA_HORARIO { get; set; }

    public virtual DbSet<COMUNA> COMUNA { get; set; }

    public virtual DbSet<EMPRESA> EMPRESA { get; set; }

    public virtual DbSet<PARAMETROS> PARAMETROS { get; set; }

    public virtual DbSet<SENTIDO> SENTIDO { get; set; }

    public virtual DbSet<SERVICIO_REALIZADO> SERVICIO_REALIZADO { get; set; }

    public virtual DbSet<TERMINAL> TERMINAL { get; set; }

    public virtual DbSet<HORARIO_CONDUCTOR> HORARIO_CONDUCTOR { get; set; }

    public virtual DbSet<USUARIOS_SISTEMA> USUARIOS_SISTEMA { get; set; }

    public virtual DbSet<TIPO_CONTRATO> TIPO_CONTRATO { get; set; }

    public virtual DbSet<ESTADO_SOLICITUD> ESTADO_SOLICITUD { get; set; }

    public virtual DbSet<SERVICIO> SERVICIO { get; set; }

    public virtual DbSet<SOLICITUD_CAMBIO_HORARIO> SOLICITUD_CAMBIO_HORARIO { get; set; }

    public virtual DbSet<TIPO_SOLICITUD_CAMBIO> TIPO_SOLICITUD_CAMBIO { get; set; }

}

}

