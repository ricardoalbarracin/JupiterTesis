using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Models.Utils;

namespace Core.Services.TRANS
{
    public interface IColaboradorComisionDAOService
    {
        Result<List<ComisionColaborador>> GetListComisiones(long? personaId);
        Result<List<ListGeneral>> GetlistColaboradoresByProyectId(long Id);
        Result<ComisionColaborador> InsComisionColaborador(ComisionColaborador comisionColaborador);
        int GetConsecutivo(int tipo);
        int UpdConsecutivo(int tipo, int valor);
        Result<ComisionColaborador> UpdSolicitudComision(long id);
        Result UpdSolicitudComision (ComisionColaborador comisionColaborador);
        Result<List<ComisionColaborador>> GetListComisionesSinDesembolso();
        Result<List<ComisionColaborador>> GetListComisionesColaborador(long? personaId);
        Result<FacturasViewModel> GetInfoComision(long ComisionId);
        Result<List<ListGeneral>> getListConcept();
        Result<Legalizaciones> InsLegalizacion(Legalizaciones legalizaciones);
        Result<FacturaIndividualViewModel> InsFacturas(FacturaIndividualViewModel facturasViewModel);
        Result UpdLegalizacion(Legalizaciones legalizaciones);
        Result UpdFactura(FacturaIndividualViewModel facturas);
        Result DeleteFactura(FacturaIndividualViewModel facturaDelete);
    }
}
