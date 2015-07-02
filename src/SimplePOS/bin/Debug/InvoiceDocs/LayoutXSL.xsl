<?xml version="1.0" encoding="UTF-8"?>


<xsl:stylesheet version="1.0"
			xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
  <xsl:template match="/">



    <Head>

      <table>

        <colgroup>
          <col width="500"/>
          <col width="600"/>
        </colgroup>

        <tr>
          <td>

            <p style="font-size:200%; padding-bottom:50px; padding-top:30px;">
              <b>RECHNUNG</b>

            </p>

          </td>
          <td>

            <img align="right" src="Zahnarztlogo.png" alt="Zahnarztlogo"/>

          </td>
        </tr>
      </table>

      <Adresse align="left">
        <P style="padding-bottom:20px;">
          <Firma style="font-size:120%">
            <b>Zahnarzt XY</b>
          </Firma>

          <br>
            <Strasse>Hauptstr. 1</Strasse>
          </br>
          <br>
            <PLZ>67547</PLZ>
            <Ort> Worms</Ort>
          </br>
        </P>

      </Adresse>

      <p style="font-size:130%">
        <b>Rechnung Nr. </b>
        <xsl:value-of select="SerializableInvoice/number"/>
      </p>

      <p align="right" style="padding-bottom:20px">
        Rechnungsdatum
        <xsl:value-of select="SerializableInvoice/strdate"/>
      </p>

    </Head>

    <b1>
      <table border="1" rules="groups">


        <colgroup>
          <col width="400"/>
          <col width="400"/>
          <col width="100"/>
          <col width="200"/>
        </colgroup>

        <thead>

          <tr>
            <th align="left">Artikel</th>
            <th align="left">Menge</th>
            <th align="left">Steuersatz</th>
            <th align="left">Gesamtpreis (EUR)</th>
          </tr>

        </thead>

        <tbody>

          <xsl:for-each select="SerializableInvoice/items/InvoiceItemSimple">

            <tr>

              <td>
                <xsl:value-of select="./name"/>
              </td>

              <td>
                <xsl:value-of select="./quantity"/>
              </td>

              <td>
                <xsl:value-of select="./tax"/>
                <Prozent> %</Prozent>
              </td>

              <td align="right">
                <xsl:value-of select="./strprice"/>
              </td>

            </tr>

          </xsl:for-each>

        </tbody>

        <tfoot></tfoot>

      </table>
    </b1>

    <b2>
      <table border="0">

        <colgroup>
          <col width="900"/>
          <col width="195"/>
        </colgroup>

        <tr>

          <th align="right">
            Rechnungsbetrag
          </th>

          <th align="right">
            <xsl:value-of select="SerializableInvoice/strtotalamount"/>
          </th>

        </tr>

      </table>
    </b2>

  </xsl:template>
</xsl:stylesheet>
