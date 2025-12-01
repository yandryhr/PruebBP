using BP.Infrastructure.Commons.Bases.Response;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public static class QuestPdfHelper
{
    public static byte[] GenerateAccountStatusPdf(List<EstadoCuentaDto> data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(9));

                page.Header().Text("ESTADO DE CUENTA");
                    

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1); // Fecha
                        columns.RelativeColumn(2); // Cliente
                        columns.RelativeColumn(1); // Número Cuenta
                        columns.RelativeColumn(1); // Tipo
                        columns.RelativeColumn(1); // Saldo Inicial
                        columns.RelativeColumn(1); // Estado
                        columns.RelativeColumn(1); // Movimiento
                        columns.RelativeColumn(1); // Saldo Disponible
                    });

                    // Encabezados
                    table.Header(header =>
                    {
                        HeaderCell(header, "Fecha");
                        HeaderCell(header, "Cliente");
                        HeaderCell(header, "Cuenta");
                        HeaderCell(header, "Tipo");
                        HeaderCell(header, "Saldo Inicial");
                        HeaderCell(header, "Estado");
                        HeaderCell(header, "Movimiento");
                        HeaderCell(header, "Saldo Disponible");
                    });

                    // Filas
                    foreach (var item in data)
                    {
                        BodyCell(table, item.Fecha+"");
                        BodyCell(table, item.Cliente);
                        BodyCell(table, item.NumeroCuenta.ToString());
                        BodyCell(table, item.Tipo);
                        BodyCell(table, item.SaldoInicial.ToString());
                        BodyCell(table, item.Estado ? "Activo" : "Inactivo");
                        BodyCell(table, item.Movimiento.ToString());
                        BodyCell(table, item.SaldoDisponible.ToString());
                    }
                });
            });
        });

        // retorna Base64
        byte[] pdfBytes = document.GeneratePdf();
        return document.GeneratePdf(); 
    }

  

    private static void HeaderCell(TableCellDescriptor table, string text)
    {
        table.Cell().Background(Colors.Grey.Lighten2)
            .Padding(6)
            .Text(text).SemiBold();
    }

    private static void BodyCell(TableDescriptor table, string text)
    {
        table.Cell().Padding(6).Text(text);
    }
}
