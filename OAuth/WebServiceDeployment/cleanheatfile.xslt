<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">

  <!-- Good old fashioned identity transform -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
  <!-- Remove the exe, the manual wix file VP.Digital.Security.OAuth.WebService.wxs includes it as part of the service install -->
  <xsl:template match="node()[@Id='VP.Digital.Security.OAuth.WebService.exe']" />
</xsl:stylesheet>