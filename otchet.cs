using System.Windows;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;


namespace zadanie1
{
   public class otchet
    {
        public static void MakeReport(string file, List<PaymentDbContext> payments, string FIO)
        {
            IEnumerable<IGrouping<CategoryDbContext, PaymentDbContext>> grouped = payments.OrderBy(x => x.Date).GroupBy(x => x.Category);
            using (WordprocessingDocument doc = WordprocessingDocument.Create(file, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                RunFonts fonts = new RunFonts()
                {
                    Ascii = "Arial",
                    EastAsia = "Arial",
                    HighAnsi = "Arial"
                };

      
                Paragraph TitleParagraph = body.AppendChild(new Paragraph(new ParagraphProperties()
                {
                    Justification = new Justification() { Val = JustificationValues.Center },
                }));
                Run TitleRun = new Run(new Text("Платежи"));
                TitleRun.PrependChild(new RunProperties()
                {
                    FontSize = new FontSize() { Val = "48" },
                    RunFonts = (RunFonts)fonts.Clone(),
                    Bold = new Bold() { Val = DocumentFormat.OpenXml.OnOffValue.FromBoolean(true) }
                });
                TitleParagraph.AppendChild(TitleRun);

               
                RunProperties GroupRunProperties = new RunProperties()
                {
                    FontSize = new FontSize() { Val = "28" },
                    RunFonts = (RunFonts)fonts.Clone(),
                    Bold = new Bold() { Val = DocumentFormat.OpenXml.OnOffValue.FromBoolean(true) }
                };

              
                RunProperties PayRunProperties = new RunProperties()
                {
                    FontSize = new FontSize() { Val = "24" },
                    RunFonts = (RunFonts)fonts.Clone()
                };

               
                ParagraphProperties pProperties = new ParagraphProperties(new Tabs(new TabStop()
                {
                    Val = TabStopValues.Right,
                    Position = 9072,
                    Leader = TabStopLeaderCharValues.Dot
                }));

                foreach (var group in grouped)
                {
                    Paragraph GroupParagraph = body.AppendChild(new Paragraph());

                    Run GroupRun = new Run(new Text(group.Key.Category));
                    GroupRun.PrependChild((RunProperties)GroupRunProperties.Clone());

                    GroupParagraph.AppendChild(GroupRun);

                    foreach (var payment in group)
                    {
                        Paragraph p = body.AppendChild(new Paragraph());
                        p.AppendChild((ParagraphProperties)pProperties.Clone());

                        Run DescRun = new Run(new Text(payment.PayDis));
                        DescRun.PrependChild((RunProperties)PayRunProperties.Clone());
                        p.AppendChild((Run)DescRun.Clone());

                        Run TabRun = new Run(new TabChar());
                        DescRun.PrependChild((RunProperties)PayRunProperties.Clone());
                        p.AppendChild((Run)TabRun.Clone());

                        Run CostRun = new Run(new Text(payment.Cost.ToString()));
                        CostRun.PrependChild((RunProperties)PayRunProperties.Clone());
                        p.AppendChild((Run)CostRun.Clone());

                    }
                }
                RunProperties AllProperties = new RunProperties()
                {
                    FontSize = new FontSize() { Val = "35" },
                    RunFonts = (RunFonts)fonts.Clone()
                };
                ParagraphProperties allProperty = new ParagraphProperties()
                {
                    Justification = new Justification{ Val = JustificationValues.Right }
                };
                Paragraph all = body.AppendChild(new Paragraph());
                all.AppendChild((ParagraphProperties)allProperty.Clone());
                Run runall = new Run(new Text(payments.Sum(pay => pay.Cost).ToString()));
                all.AppendChild((Run)runall.Clone());
                runall.PrependChild((RunProperties)AllProperties.Clone());
               
                doc.Close();
                
            }
        }
    }
}
