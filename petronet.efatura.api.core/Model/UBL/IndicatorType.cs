﻿using System.Xml.Serialization;

namespace petronet.efatura.api.core.Model.UBL {

    [XmlType(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:UnqualifiedDataTypes-2")]
    public partial class IndicatorType {
        [XmlText()]
        public bool Value { get; set; }
    }

}