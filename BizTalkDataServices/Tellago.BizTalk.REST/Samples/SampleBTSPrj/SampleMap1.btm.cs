namespace SampleBTSPrj {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SampleBTSPrj.SampleSchema", typeof(SampleBTSPrj.SampleSchema))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SampleBTSPrj.Schema1", typeof(SampleBTSPrj.Schema1))]
    public sealed class SampleMap1 : Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0"" version=""1.0"" xmlns:ns0=""http://SampleBTSPrj.Schema1"" xmlns:s0=""http://SampleBTSPrj.SampleSchema"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Root"" />
  </xsl:template>
  <xsl:template match=""/s0:Root"">
    <ns0:Field>
      <xsl:value-of select=""Field/text()"" />
    </ns0:Field>
  </xsl:template>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"SampleBTSPrj.SampleSchema";
        
        private const string _strTrgSchemasList0 = @"SampleBTSPrj.Schema1";
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"SampleBTSPrj.SampleSchema";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"SampleBTSPrj.Schema1";
                return _TrgSchemas;
            }
        }
    }
}
