<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">

  <!-- Good old fashioned identity transform -->
  <xsl:template match="@*|*">
    <xsl:copy>
      <xsl:apply-templates select="@*" />
      <xsl:apply-templates select="*" />
    </xsl:copy>
  </xsl:template>

  <!-- Remove all .cs files -->
  <xsl:key name="cs-search" match="wix:Component[substring(wix:File/@Source,  (string-length(wix:File/@Source) - string-length('.cs')) + 1) = '.cs']" use="@Id"/>
  <xsl:template match="wix:Component[key('cs-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('cs-search', @Id)]"/>

  <!-- Remove all .csproj files -->
  <xsl:key name="csproj-search" match="wix:Component[contains(wix:File/@Source, '.csproj')]" use="@Id"/>
  <xsl:template match="wix:Component[key('csproj-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('csproj-search', @Id)]"/>

  <!-- Remove Web.debug.config -->
  <xsl:key name="web-debug-search" match="wix:Component[contains(wix:File/@Source, 'Web.Debug.config')]" use="@Id"/>
  <xsl:template match="wix:Component[key('web-debug-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('web-debug-search', @Id)]"/>

    <!-- Remove Web.release.config -->
  <xsl:key name="web-release-search" match="wix:Component[contains(wix:File/@Source, 'Web.Release.config')]" use="@Id"/>
  <xsl:template match="wix:Component[key('web-release-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('web-release-search', @Id)]"/>

  <!-- Remove the App_start Directory -->
  <xsl:key name="app-start-search" match="wix:Component[ancestor::wix:Directory/@Name = 'App_Start']" use="@Id"/>
  <xsl:template match="wix:Directory[@Name = 'App_Start']"/>
  <xsl:template match="wix:ComponentRef[key('app-start-search', @Id)]"/>

  <!-- Remove the Models Directory -->
  <xsl:key name="models-search" match="wix:Component[ancestor::wix:Directory/@Name = 'Models']" use="@Id"/>
  <xsl:template match="wix:Directory[@Name = 'Models']"/>
  <xsl:template match="wix:ComponentRef[key('models-search', @Id)]"/>


  <!-- Remove the obj Directory -->
  <xsl:key name="obj-search" match="wix:Component[ancestor::wix:Directory/@Name = 'obj']" use="@Id"/>
  <xsl:template match="wix:Directory[@Name = 'obj']"/>
  <xsl:template match="wix:ComponentRef[key('obj-search', @Id)]"/>

  <!-- Remove the Properties Directory -->
  <xsl:key name="properties-search" match="wix:Component[ancestor::wix:Directory/@Name = 'Properties']" use="@Id"/>
  <xsl:template match="wix:Directory[@Name = 'Properties']"/>
  <xsl:template match="wix:ComponentRef[key('properties-search', @Id)]"/>

</xsl:stylesheet>