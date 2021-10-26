
namespace WebApplication1.Models.Commons
{
    public enum RolesPermisos
    {
        #region Permisos IntranetBuses

        IntranetBuses_ZonaAdministrativa_Visualizar = 2,
        IntranetBuses_ZonaAdministrativa_Crear = 3,
        IntranetBuses_ZonaAdministrativa_Editar = 4,
        IntranetBuses_ZonaAdministrativa_Eliminar = 5,

        IntranetBuses_TipoBus_Visualizar = 6,
        IntranetBuses_TipoBus_Crear = 7,
        IntranetBuses_TipoBus_Editar = 8,
        IntranetBuses_TipoBus_Eliminar = 9,

        IntranetBuses_TipoDia_Visualizar = 10,
        IntranetBuses_TipoDia_Crear = 11,
        IntranetBuses_TipoDia_Editar = 12,
        IntranetBuses_TipoDia_Eliminar = 13,
        IntranetBuses_TipoDia_CrearConjuntoPeriodo = 14,

        IntranetBuses_Terminal_Visualizar = 15,
        IntranetBuses_Terminal_Crear = 16,
        IntranetBuses_Terminal_Editar = 17,
        IntranetBuses_Terminal_Eliminar = 18,

        IntranetBuses_Servicio_Visualizar = 19,
        IntranetBuses_Servicio_Crear = 20,
        IntranetBuses_Servicio_Editar = 21,
        IntranetBuses_Servicio_Eliminar = 22,

        IntranetBuses_ParametroServicioTipoDiaSentido_Visualizar = 23,
        IntranetBuses_ParametroServicioTipoDiaSentido_Crear = 24,
        IntranetBuses_ParametroServicioTipoDiaSentido_Editar = 25,
        IntranetBuses_ParametroServicioTipoDiaSentido_Eliminar = 26,

        IntranetBuses_ParametroServicioTipoDiaSentidoMH_Visualizar = 27,
        IntranetBuses_ParametroServicioTipoDiaSentidoMH_Crear = 28,
        IntranetBuses_ParametroServicioTipoDiaSentidoMH_Editar = 29,
        IntranetBuses_ParametroServicioTipoDiaSentidoMH_Eliminar = 30,

        IntranetBuses_ParametroServicioTipoDiaSentidoPD_Visualizar = 31,
        IntranetBuses_ParametroServicioTipoDiaSentidoPD_Crear = 32,
        IntranetBuses_ParametroServicioTipoDiaSentidoPD_Editar = 33,
        IntranetBuses_ParametroServicioTipoDiaSentidoPD_Eliminar = 34,

        IntranetBuses_Anexos_Visualizar = 35,
        IntranetBuses_Anexos_Crear = 36,
        IntranetBuses_Anexos_Editar = 37,
        IntranetBuses_Anexos_Eliminar = 38,

        IntranetBuses_SimulacionBuses_Proyectos_Visualizar = 40,
        IntranetBuses_SimulacionBuses_Proyectos_Crear = 41,
        IntranetBuses_SimulacionBuses_Proyectos_Editar = 42,
        IntranetBuses_SimulacionBuses_Proyectos_Eliminar = 43,

        IntranetBuses_SimulacionBuses_NuevaSimulacion_Visualizar = 44,
        IntranetBuses_SimulacionBuses_NuevaSimulacion_Crear = 45,
        IntranetBuses_SimulacionBuses_NuevaSimulacion_Editar = 46,

        #endregion

        #region Permisos IntranetICRP

        IntranetICRP_Medicion_Servicios_Visualizar = 7,
        IntranetICRP_Reporte_PPU_Visualizar = 9,
        IntranetICRP_Reporte_Períodos_Visualizar = 10,
        IntranetICRP_Mantencion_Anexo5_Visualizar = 28,
        IntranetICRP_Carga_Anexo5_Visualizar = 29,
        IntranetICRP_UF_Visualizar = 30,
        #endregion

        #region Permisos Recursos Humanos(Planillas)
        IntranetRRHHPlanillas_visualizar_dashboard = 19,

        IntraneRevisionPlanillas_ver_revision_mensual = 13,
        IntraneRevisionPlanillas_ver_cuadratura_dif = 14,
        IntraneRevisionPlanillas_ver_no_cubiertas = 15,
        IntraneRevisionPlanillas_verEventosPorPeriodo = 24,
        IntraneRevisionPlanillas_ver_cuadratura_aprobacion = 16,
        IntraneRevisionPlanillas_ver_seguimiento_planillas = 17,
        IntraneRevisionPlanillas_ver_bitacora_usuario = 18,

        IntraneRevisionPlanillas_visualizar_reportes = 12,

        IntraneRevisionPlanillas_visualizar_planillas = 11,
        IntraneRevisionPlanillas_aprobar_planilla = 1,
        IntraneRevisionPlanillas_diferencias_planilla = 2,
        IntraneRevisionPlanillas_ver_planilla = 3,
        IntraneRevisionPlanillas_modificar_planilla = 4,
        IntraneRevisionPlanillas_ingresar_planilla_manual = 5,
        IntraneRevisionPlanillas_anular_planilla_manual = 6,
        IntraneRevisionPlanillas_exportar_planillas = 7,
        IntraneRevisionPlanillas_check_oopp = 8,
        IntraneRevisionPlanillas_check_informatica = 9,
        IntraneRevisionPlanillas_insertar_diferencias = 10,

        IntraneRevisionPlanillas_agrega_contingencia = 20,
        IntraneRevisionPlanillas_ver_contingencia = 21,
        IntraneRevisionPlanillas_modificar_contingencia = 22,
        IntraneRevisionPlanillas_anular_contingencia = 23,

        IntraneRevisionPlanillas_cerrar_semana = 24,
        IntraneRevisionPlanillas_abrir_semana = 25,
        IntraneRevisionPlanillas_ver_cierre_semanas = 26

        #endregion
    }
}