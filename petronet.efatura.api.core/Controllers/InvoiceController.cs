﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using petronet.efatura.api.core.Integration.Command;
using petronet.efatura.api.core.Integration.Command.GetUBL;
using petronet.efatura.api.core.Integration.Command.Interfaces;
using UBL = petronet.efatura.api.core.Model.UBL;
using petronet.efatura.api.core.ViewModel;
using Exception = System.Exception;

namespace petronet.efatura.api.core.Controllers {

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase {
        private IMapper _mapper;
        private IConfiguration Configuration;

        public InvoiceController(IConfiguration configuration, IMapper mapper) {
            this._mapper = mapper;
            this.Configuration = configuration;
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        [Produces("application/xml")]
        public IActionResult Get() {
            var ii = new UBL.InvoiceInfo {
                Invoice = new UBL.InvoiceType {
                    ID = new UBL.IDType {
                        schemeID = "VKN",
                        Value = "123123"
                    },
                    ProfileID = new UBL.ProfileIDType { Value = "TICARIFATURA" },
                    UBLVersionID = new UBL.UBLVersionIDType { Value = "2.1" },
                    CustomizationID = new UBL.CustomizationIDType { Value = "TR1.2" },
                    IssueDate = new UBL.IssueDateType { Value = DateTime.Parse("2009-01-05") },
                    IssueTime = new UBL.IssueTimeType { Value = DateTime.Parse("14:42:00") },
                    InvoiceTypeCode = new UBL.InvoiceTypeCodeType { Value = "SATIS" },
                    Note = new[]
                    {
                        new UBL.NoteType {Value = "notum not olsun 1"},
                        new UBL.NoteType {Value = "notum not olsun 2"}
                    },
                    DocumentCurrencyCode = new UBL.DocumentCurrencyCodeType { Value = "TRY" },
                    LineCountNumeric = new UBL.LineCountNumericType() { Value = 2 },
                    AdditionalDocumentReference = new[]
                    {
                        new UBL.DocumentReferenceType
                        {
                            ID = new UBL.IDType {Value = "703a3cda-be8d-4964-bfa2-29b63c14e712"},
                            IssueDate = new UBL.IssueDateType {Value = DateTime.Now},
                            DocumentType = new UBL.DocumentTypeType {Value = "Xslt"}
                        },
                    },
                    Signature = new[]
                    {
                        new UBL.SignatureType1
                        {
                            ID = new UBL.IDType()
                            {
                                schemeID = "VKN",
                                Value = "9000068418"
                            }
                        }
                    },
                    AccountingSupplierParty = new UBL.SupplierPartyType {
                        Party = new UBL.PartyType {
                            WebsiteURI = new UBL.WebsiteURIType { Value = "www.ulukom.com.tr" },
                            PartyIdentification = new[]
                            {
                                new UBL.PartyIdentificationType
                                {
                                    ID = new UBL.IDType {schemeID = "VKN", Value = "9000068418"}
                                },
                                new UBL.PartyIdentificationType
                                {
                                    ID = new UBL.IDType {schemeID = "TICARETSICILNO", Value = "999999"}
                                },
                                new UBL.PartyIdentificationType
                                {
                                    ID = new UBL.IDType {schemeID = "MERSISNO", Value = "125363"}
                                }
                            },
                            PartyName = new UBL.PartyNameType {
                                Name = new UBL.NameType1 {
                                    Value = "TST_ULUKOM BİLGİSAYAR YAZILIM DONANIM DAN.SAN.VE TİC. LTD.ŞTİ."
                                }
                            },
                            PostalAddress = new UBL.AddressType {
                                StreetName = new UBL.StreetNameType { Value = "1.TAŞOCAĞI CD. BU.... M.KÖY" },
                                CitySubdivisionName = new UBL.CitySubdivisionNameType { Value = "Maslak" },
                                CityName = new UBL.CityNameType { Value = "İstanbul" },
                                Country = new UBL.CountryType {
                                    Name = new UBL.NameType1 { Value = "Türkiye" }
                                }
                            },
                            PartyTaxScheme = new UBL.PartyTaxSchemeType {
                                TaxScheme = new UBL.TaxSchemeType { Name = new UBL.NameType1 { Value = "BEYKOZ" } }
                            },
                            Contact = new UBL.ContactType {
                                Telephone = new UBL.TelephoneType { Value = "0 212 212 26 92" },
                                Telefax = new UBL.TelefaxType { Value = "0 212 212 26 67" },
                                ElectronicMail = new UBL.ElectronicMailType { Value = "ulukom@ulukom.com.tr" },
                            }
                        },
                    },
                    AccountingCustomerParty = new UBL.CustomerPartyType {
                        Party = new UBL.PartyType {
                            WebsiteURI = new UBL.WebsiteURIType {
                                Value = "www.ulukom.com.tr"
                            },
                            PartyIdentification = new[]
                            {
                                new UBL.PartyIdentificationType
                                {
                                    ID = new UBL.IDType
                                    {
                                        schemeID = "VKN",
                                        Value = "9000068418"
                                    }
                                }
                            },
                            PartyName = new UBL.PartyNameType {
                                Name = new UBL.NameType1 {
                                    Value = "ULUKOM  LOGISTICS"
                                }
                            },
                            PostalAddress = new UBL.AddressType {
                                StreetName = new UBL.StreetNameType { Value = "1.TAŞOCAĞI CD. BU.... M.KÖY" },
                                CitySubdivisionName = new UBL.CitySubdivisionNameType { Value = "Maslak" },
                                CityName = new UBL.CityNameType { Value = "İstanbul" },
                                Country = new UBL.CountryType {
                                    Name = new UBL.NameType1 { Value = "Türkiye" }
                                }
                            },
                            PartyTaxScheme = new UBL.PartyTaxSchemeType {
                                TaxScheme = new UBL.TaxSchemeType { Name = new UBL.NameType1 { Value = "BEYKOZ" } }
                            },
                            Contact = new UBL.ContactType {
                                Telephone = new UBL.TelephoneType { Value = "0 212 212 26 92" },
                                Telefax = new UBL.TelefaxType { Value = "0 212 212 26 67" },
                                ElectronicMail = new UBL.ElectronicMailType { Value = "ulukom@ulukom.com.tr" },
                            }
                        }
                    },
                    TaxTotal = new[]
                    {
                        new UBL.TaxTotalType()
                        {
                            TaxAmount = new UBL.TaxAmountType() {Value = 900, currencyID = "TRY"},
                            TaxSubtotal = new[]
                            {
                                new UBL.TaxSubtotalType()
                                {
                                    TaxableAmount = new UBL.TaxableAmountType() {currencyID = "TRY", Value = 5000},
                                    Percent = new UBL.PercentType1() {Value = 18},
                                    TaxCategory = new UBL.TaxCategoryType()
                                    {
                                        TaxScheme = new UBL.TaxSchemeType()
                                        {
                                            Name = new UBL.NameType1() {Value = "KDV"},
                                            TaxTypeCode = new UBL.TaxTypeCodeType() {Value = "015"}
                                        }
                                    }
                                }
                            }
                        },
                        new UBL.TaxTotalType()
                        {
                            TaxAmount = new UBL.TaxAmountType() {Value = 160, currencyID = "TRY"},
                            TaxSubtotal = new[]
                            {
                                new UBL.TaxSubtotalType()
                                {
                                    TaxableAmount = new UBL.TaxableAmountType() {currencyID = "TRY", Value = 2000},
                                    Percent = new UBL.PercentType1() {Value = 8},
                                    TaxCategory = new UBL.TaxCategoryType()
                                    {
                                        TaxScheme = new UBL.TaxSchemeType()
                                        {
                                            Name = new UBL.NameType1() {Value = "KDV"},
                                            TaxTypeCode = new UBL.TaxTypeCodeType() {Value = "015"}
                                        }
                                    }
                                }
                            }
                        }
                    },
                    LegalMonetaryTotal = new UBL.MonetaryTotalType() {
                        LineExtensionAmount = new UBL.LineExtensionAmountType() { Value = 7000, currencyID = "TRY" },
                        TaxExclusiveAmount = new UBL.TaxExclusiveAmountType() { Value = 7000, currencyID = "TRY" },
                        TaxInclusiveAmount = new UBL.TaxInclusiveAmountType() { Value = 1060, currencyID = "TRY" },
                        AllowanceTotalAmount = new UBL.AllowanceTotalAmountType() { Value = 0, currencyID = "TRY" },
                        PayableAmount = new UBL.PayableAmountType() { Value = 8060, currencyID = "TRY" }
                    },
                    InvoiceLine = new[]
                    {
                        new UBL.InvoiceLineType()
                        {
                            ID = new UBL.IDType(){Value = "1"},
                            Note = new []{ new UBL.NoteType(){Value = "WAREHOUSE HANDLING COSTS ACTUAL" } },
                            InvoicedQuantity = new UBL.InvoicedQuantityType(){Value =1,unitCode = "NIU"},
                            LineExtensionAmount = new UBL.LineExtensionAmountType(){Value = 5000,currencyID = "TRY"},
                            TaxTotal = new UBL.TaxTotalType()
                            {
                                TaxAmount = new UBL.TaxAmountType(){Value = 900, currencyID = "TRY"},
                                TaxSubtotal = new []
                                {
                                    new UBL.TaxSubtotalType()
                                    {
                                        TaxableAmount = new UBL.TaxableAmountType(){Value = 5000, currencyID = "TRY"},
                                        TaxAmount = new UBL.TaxAmountType(){ Value = 900, currencyID = "TRY"},
                                        Percent = new UBL.PercentType1(){Value = 18},
                                        TaxCategory = new UBL.TaxCategoryType()
                                        {
                                            TaxExemptionReason = new UBL.TaxExemptionReasonType(),
                                            TaxScheme = new UBL.TaxSchemeType()
                                            {
                                                Name =  new UBL.NameType1(){ Value = "KDV"},
                                                TaxTypeCode = new UBL.TaxTypeCodeType(){Value = "015"}
                                            }
                                        }
                                    }
                                }
                            },
                            Item = new UBL.ItemType()
                            {
                                Description = new UBL.DescriptionType(){Value = "WAREHOUSE HANDLING COSTS ACTUAL"},
                                Name = new UBL.NameType1(){Value = "WAREHOUSE HANDLING COSTS ACTUAL"},
                                BrandName = new UBL.BrandNameType(){Value = "WAREHOUSE HANDLING COSTS ACTUAL"},
                                ModelName = new UBL.ModelNameType(){Value = "WAREHOUSE HANDLING COSTS ACTUAL"},
                                SellersItemIdentification = new UBL.ItemIdentificationType()
                                {
                                    ID = new UBL.IDType(){Value = "1210.20.10"},
                                }
                            },
                            Price = new UBL.PriceType()
                            {
                                PriceAmount = new UBL.PriceAmountType()
                                {
                                    Value = 5000,
                                    currencyID = "TRY"
                                }
                            }
                        }
                    }
                }
            };

            return Ok(ii);
        }


        [HttpPost("uuid:string")]
        public async Task<IActionResult> Get(string uuid, [FromHeader] ServiceInfo serviceInfo) {
            ICommandGetUBL cmd = null;

            if (!ModelState.IsValid) {
                foreach (var modelState in ModelState.Values)
                    foreach (var error in modelState.Errors)
                        Console.WriteLine(error);
                throw new Exception("Oluşan hata sayısı: " + ModelState.ErrorCount);
            }

            switch (serviceInfo.Integrator) {
                case Integrator.Uyumsoft:
                    throw new NotImplementedException("Uyumsoft yapılmadı");

                case Integrator.EFinans:
                    throw new NotImplementedException("EFinans yapılmadı");

                case Integrator.ING:
                    cmd = new IngGetUBL(new[] { uuid }, this._mapper);
                    break;

                case Integrator.DigitalPlanet:
                    throw new NotImplementedException("DP yapılmadı");

                case Integrator.IsNet:
                    throw new NotImplementedException("ISNET uygulanmadı");

                default:
                    throw new ArgumentNullException("Command null kalmış. Demek entegratör tanımlı değil.");
                    break;
            }

            cmd.ServiceInfo = serviceInfo;
            await cmd.Execute();
            var result = await cmd.Result;

            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/xml")]
        //[Produces("application/xml")]
        public async Task<IActionResult> Post([FromBody] UBL.InvoiceType invoice, [FromHeader] ServiceInfo serviceInfo) {

            ICommandSendUBL cmd = null;

            if (!ModelState.IsValid) {
                foreach (var modelState in ModelState.Values)
                    foreach (var error in modelState.Errors)
                        Console.WriteLine(error);
                throw new Exception("Oluşan hata sayısı: " + ModelState.ErrorCount);
            }

            var validationErrors = ValidateReceivedData(serviceInfo.Integrator, serviceInfo);
            if (!string.IsNullOrEmpty(validationErrors)) {
                return BadRequest(validationErrors);
            }

            switch (serviceInfo.Integrator) {
                case Integrator.Uyumsoft:
                    cmd = new UyumsoftSendEInvoice(invoice, this._mapper);
                    break;

                case Integrator.EFinans:
                    cmd = new EFinansSendEInvoice(invoice);
                    break;

                case Integrator.ING:
                    cmd = new IngSendEInvoice(invoice);
                    break;

                case Integrator.DigitalPlanet:
                    cmd = new DijitalPlanetSendEInvoice(invoice);
                    break;

                case Integrator.IsNet:
                    cmd = new IsnetSendEInvoice(invoice);
                    break;

                default:
                    throw new ArgumentNullException("Command null kalmış. Demek entegratör tanımlı değil.");
                    break;
            }

            cmd.ServiceInfo = serviceInfo;
            await cmd.Execute();
            var result = await cmd.Result;

            return Ok(result);
        }

        private string ValidateReceivedData(Integrator serviceInfoIntegrator, ServiceInfo serviceInfo) {
            string errors = string.Empty;
            switch (serviceInfo.Integrator) {
                case Integrator.Uyumsoft:
                    break;

                case Integrator.EFinans:
                    break;

                case Integrator.ING:
                    break;

                case Integrator.DigitalPlanet:
                    if (string.IsNullOrEmpty(serviceInfo.ReceiverPostboxName))
                        errors += "Dijital planet üstünden fatura gönderebilmek için ReceiverPostboxName bilgisi gönderilmelidir!";
                    break;

                case Integrator.IsNet:
                    break;

                default:
                    errors = string.Empty;
                    break;
            }

            return errors;
        }

        //private static IIntegration GetUyumsoftServiceProxy() {
        //    BasicHttpBinding basicHttpBinding = null;
        //    EndpointAddress endpointAddress = null;
        //    ChannelFactory<uyumsoft.IIntegration> factory = null;
        //    uyumsoft.IIntegration serviceProxy = null;

        //    //uyumsoft.IntegrationClient ic = GetUyumsoftClient(false);
        //    basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
        //    basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

        //    factory = new ChannelFactory<uyumsoft.IIntegration>(basicHttpBinding,
        //        new EndpointAddress(new Uri("https://efatura-test.uyumsoft.com.tr/Services/Integration")));
        //    factory.Credentials.UserName.UserName = "Uyumsoft";
        //    factory.Credentials.UserName.Password = "Uyumsoft";
        //    serviceProxy = factory.CreateChannel();
        //    ((ICommunicationObject)serviceProxy).Open();
        //    var opContext = new OperationContext((IClientChannel)serviceProxy);
        //    var prevOpContext = OperationContext.Current; // Optional if there's no way this might already be set
        //    OperationContext.Current = opContext;
        //    return serviceProxy;
        //}

        //private IntegrationClient GetUyumsoftClient(bool isProduction, int timeout) {
        //    var serviceAddress = isProduction
        //        ? "https://efatura.uyumsoft.com.tr/Services/Integration"
        //        : "https://efatura-test.uyumsoft.com.tr/Services/Integration";
        //    //: "http://efatura-test.uyumsoft.com.tr/services/BasicIntegration";


        //    var binding = new BasicHttpBinding() {
        //        MaxReceivedMessageSize = int.MaxValue,
        //        Security =
        //        {
        //            Transport = new HttpTransportSecurity() {
        //                ClientCredentialType = HttpClientCredentialType.None
        //            },
        //            Mode = BasicHttpSecurityMode.TransportWithMessageCredential
        //        },
        //    };

        //    var endPointTest = new EndpointAddress(serviceAddress);
        //    binding.CloseTimeout = TimeSpan.MaxValue;

        //    var client = new uyumsoft.IntegrationClient(binding, endPointTest);
        //    client.ClientCredentials.UserName.UserName = "Uyumsoft";
        //    client.ClientCredentials.UserName.Password = "Uyumsoft";
        //    return client;
        //}

        [HttpGet("{id}")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult Get(int id) {
            XmlSerializer ser = new XmlSerializer(typeof(UBL.InvoiceType));
            using (var stream = System.IO.File.Open(@"C:\Users\cemt\Downloads\9b9d0c93-5e18-4b65-88e9-39e47cf6e651.xml",
                FileMode.Open)) {
                var obj = ser.Deserialize(stream) as UBL.InvoiceType;
                obj.UBLVersionID.Value = "3333";
                stream.Close();
                return Ok(obj);
            }
        }
    }
}