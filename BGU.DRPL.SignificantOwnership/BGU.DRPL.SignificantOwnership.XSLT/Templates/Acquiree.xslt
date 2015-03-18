﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:template match="Acquiree" name="tmplAcquiree" mode="viewAcquiree">
    <table width="100%">
      <tr>
        <td width="50%">
          <b>1.	Інформація про юридичну особу</b>
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td width="50%">
          1.1.	Найменування юридичної особи
        </td>
        <td>
          <b>
            <xsl:value-of select="Name" />
          </b>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          1.2.	Місцезнаходження юридичної особи
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <table width="100%" class="infoTable">
            <tr>
              <td colspan="4" align="center">
                <b>Місцезнаходження</b>
              </td>
            </tr>
            <tr>
              <td>
                <b>Країна</b>
              </td>
              <td>
                <b>Населений пункт</b>
              </td>
              <td>
                <b>Індекс</b>
              </td>
              <td>
                <b>Вулиця, номер будинку, приміщення</b>
              </td>
            </tr>
            <tr>
              <td>
                <xsl:value-of select="Address/Country/CountryNameUkr"/> (<xsl:value-of select="Address/Country/CountryISONr"/>)
              </td>
              <td>
                <xsl:value-of select="Address/City"/>
              </td>
              <td>
                <xsl:value-of select="Address/ZipCode"/>
              </td>
              <td>
                <xsl:if test="Address/Street!=''">
                  <xsl:value-of select="Address/Street"/>
                </xsl:if>
                <xsl:if test="Address/HouseNr!=''">
                  , буд.<xsl:value-of select="Address/HouseNr"/>
                </xsl:if>
                <xsl:if test="Address/ApptOfficeNr!=''">
                  , кв./офіс <xsl:value-of select="Address/ApptOfficeNr"/>
                </xsl:if>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td>1.3.	Ідентифікаційний номер юридичної особи</td>
        <td>
          <b>
            <i>
              <xsl:value-of select="TaxCodeOrHandelsRegNr"/>
            </i>
          </b>
        </td>
      </tr>
      <tr>
        <td colspan="2">1.4.	Орган, що здійснив державну реєстрацію юридичної особи</td>
      </tr>
      <tr>
        <td colspan="2">
          <table width="100%" style="border: 0px solid rgb(255, 255, 255); font-size: 70%; margin-left: 10px; border-collapse: collapse; background-color: rgb(240, 240, 224);">
            <tr>
              <td>
                <b>Країна</b>
              </td>
              <td>
                <b>Повне найменування органу</b>
              </td>
            </tr>
            <tr>
              <td>
                <xsl:value-of select="Registrar/JurisdictionCountry/CountryNameUkr"/> (<xsl:value-of select="Registrar/JurisdictionCountry/CountryISONr"/>)
              </td>
              <td>
                <xsl:value-of select="Registrar/RegistrarName"/>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <!-- -->
      <!--
      <tr>
        <td>???!</td>
        <td>
          <b>
            <i>
              <xsl:value-of select="???!"/>
            </i>
          </b>
        </td>
      </tr> -->
    </table>
  </xsl:template>
</xsl:stylesheet>