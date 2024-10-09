using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Sample.QuestPdf.WebApi.Controllers;

[Route("api/[controller]")]
public class SampleController : Controller
{
	[HttpGet]
	public Task<IActionResult> GetSampleQuestPdf()
	{
		var result = GeneratePdf();

		return Task.FromResult<IActionResult>(File(result, "application/octet-stream", "arquivo.pdf"));
	}

	private byte[] GeneratePdf()
	{
		QuestPDF.Settings.License = LicenseType.Community;

		var document = Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(2, Unit.Centimetre);
				page.PageColor(Colors.White);
				page.DefaultTextStyle(x => x.FontSize(20));

				page.Header()
					.Text("Primeiro PDF!")
					.FontSize(20)
					.Bold()
					.AlignCenter();

				page.Content()
					.PaddingVertical(1, Unit.Centimetre)
					.Column(x =>
					{
						x.Spacing(20);

						x.Item().Text("Exemplo do QuestPDF");
						x.Item().Element(ComposeTabela);
					});

				page.Footer()
					.AlignCenter()
					.Text(x =>
					{
						x.DefaultTextStyle(x => x.FontSize(10));
						x.Span("Página ");
						x.CurrentPageNumber();
					});
			});
		});

		var result = document.GeneratePdf();

		return result;
	}

	void ComposeTabela(IContainer container)
	{
		container.Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
			});

			table.Header(header =>
			{
				header.Cell().Text("Produto").Bold();
				header.Cell().Text("Quantidade").Bold();
				header.Cell().Text("Preço").Bold();
			});

			// Dados de exemplo
			table.Cell().Text("Produto A");
			table.Cell().Text("10");
			table.Cell().Text("R$ 100,00");

			table.Cell().Text("Produto B");
			table.Cell().Text("5");
			table.Cell().Text("R$ 50,00");

			table.Cell().Text("Produto C");
			table.Cell().Text("2");
			table.Cell().Text("R$ 200,00");
		});
	}
}