<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
      xmlns:user="my_scripts">
<msxsl:script language="JScript" implements-prefix="user">
<![CDATA[ 
	var curr_item, curr_style, curr_kei;
	var count = 0;
	var sum = 0;
	curr_style = "content";
	function add(kol) {
		sum += kol;
		count++;
		return sum;
   }
   function get_class(kei) {
		curr_kei = kei;
		return curr_style;
   }
   function test(str){
		if(curr_item == str && count >= 0){ 
			return 1;
		}
		if(curr_item != str && count != 0){ 
			set_class();
			curr_item = str;
			return 0;
		}
		if(curr_item != str && count == 0){ 
			curr_item = str;
			return 1;
		}
   }
   function set_class(){
		if(curr_style == "contenttwo"){ 
				curr_style = "content";
			}
			else{ 
				curr_style = "contenttwo";
			}
   }
   function get_count(){
		var c = count;
		count = 0;
		return c;
   }
   function get_sum(){
		var c = sum;
		sum = 0;
		c = Math.floor(c*1000)/1000;
		return c;
   }
   function get_kei(){
		return curr_kei;
   }
   function get_foot_class(inv){
        if(inv) return curr_style;
		if(curr_style == "contenttwo"){ 
				return "content";
			}
			else{ 
				return "contenttwo";
			}
   }
   function getColor(cod){
		if(cod == 0) return "#FF0000";
		if(cod == 3) return "#0000FF";
		return "#000000";
   }
]]>
</msxsl:script>
  <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
      <html>
        <head>
		<META HTTP-EQUIV="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="stylesheet" href="..\..\..\config\css\styles.css"/>
          <title><xsl:value-of select="//hdr/@title"/></title>
		  <script language="javascript">
			function outliner () {
				oMe = window.event.srcElement;
				//get child element
				var child = document.all[oMe.getAttribute("child",false)];
				//if child element exists, expand or collapse it.
				if (null != child)
					child.className = child.className == "collapsed" ? "expanded" : "collapsed";
				}

			function changepic() {
			uMe = window.event.srcElement;
			var check = uMe.src.toLowerCase();
			if (check.lastIndexOf("plus") != -1)
			{
				uMe.src = "..\..\..\config\css\minus.gif"
			}
			else
			{
				uMe.src = "..\..\..\config\css\plus.gif"
			}
          }
        </script>
		</head>
          <body topmargin="0" leftmargin="0" rightmargin="0" onclick="outliner()">
			<h2><xsl:value-of select="//hdr/@title"/></h2>
            <h1>Простые материалы</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Норматив</td>
				<td nowrap="1" class="header">Марка</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//mid3">
                <xsl:sort select="@obozn"/>
				<xsl:sort select="../@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Сложные материалы</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//cid">
                <xsl:sort select="@obozn"/>
				<xsl:sort select="../@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Покупные комплект 1</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//bid1">
                <xsl:sort select="@obozn"/>
				<xsl:sort select="../@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Покупные комплект 2</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//bid2">
                <xsl:sort select="@obozn"/>
				<xsl:sort select="../@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Поковки</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//pok">
                <xsl:sort select="@obozn"/>
				<xsl:sort select="../@obozn"/>
              </xsl:apply-templates>
            </table>                                                  
          </body>
        
      </html>

    </xsl:template>

  <xsl:template match="gid">
    <xsl:value-of select="concat(' ',@naimen)"/>
  </xsl:template>

  <xsl:template match="did">
    <xsl:value-of select="concat(' ',@obozn)"/>[<xsl:value-of select="@naimen"/>]<xsl:if test="@num_kol != '1'">[<xsl:value-of select="@num_kol"/>]</xsl:if><xsl:if test="../@id"> -><xsl:apply-templates select=".."/></xsl:if>
  </xsl:template>
  <xsl:template match="oid">
	<xsl:variable name="col" select="user:getColor(number(@code))"/>
    <font><xsl:attribute name="color"><xsl:value-of select="$col"/></xsl:attribute>
		<xsl:value-of select="concat(' ',@obozn)"/>[<xsl:value-of select="@naimen"/>]
		<xsl:if test="@num_kol != '1'">[<xsl:value-of select="@num_kol"/>]</xsl:if>
	</font>	-><xsl:apply-templates select=".."/>
  </xsl:template>
  <xsl:template match="lid">
    <xsl:variable name="col" select="user:getColor(number(@code))"/>
    <font><xsl:attribute name="color"><xsl:value-of select="$col"/></xsl:attribute>
	<xsl:value-of select="concat(' ',@obozn)"/>[<xsl:value-of select="@naimen"/>]
	<xsl:if test="@num_kol != '1'">[<xsl:value-of select="@num_kol"/>]</xsl:if>
    </font> -><xsl:apply-templates select=".."/>
  </xsl:template>
  <xsl:template match="mid3">  
  <xsl:call-template name="common">
      <xsl:with-param name="isMid3" select="'yes'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="cid"> 
  <xsl:call-template name="common">
      <xsl:with-param name="isMid3" select="'no'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="bid1">  
  <xsl:call-template name="common">
      <xsl:with-param name="isMid3" select="'no'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="bid2">  
  <xsl:call-template name="common">
      <xsl:with-param name="isMid3" select="'no'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="pok">
    <xsl:call-template name="common">
      <xsl:with-param name="isMid3" select="'no'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="common">
      <xsl:param name="isMid3"/> 
      <xsl:variable name="test" select="user:test(concat('',@obozn))"/>
      <xsl:variable name="source-id" select="generate-id(.)"/>
      <xsl:if test="$test = 0">
    	<tr valign="top">
			<xsl:variable name="foot_style" select="user:get_foot_class()"/>
    		<td colspan="7"><xsl:attribute name="class"><xsl:value-of select="$foot_style"/></xsl:attribute>
            Количество: <xsl:value-of select="user:get_count()"/>. 
            Общая сумма: <xsl:value-of select="user:get_sum()"/> <xsl:value-of select="concat(' ',user:get_kei())"/>.
            </td>
    	</tr>
      </xsl:if>
	  <xsl:variable name="sum" select="user:add(number(@sum))"/>
      <xsl:variable name="curr_style" select="user:get_class(concat('',@kei))"/>
      <tr><xsl:attribute name="class"><xsl:value-of select="$curr_style"/></xsl:attribute>
         <td><img border="0" alt="развернуть/свернуть секцию" height="11" onclick="changepic()" src="..\..\..\config\css\plus.gif" width="9"><xsl:attribute name="child">src<xsl:value-of select="$source-id"/></xsl:attribute></img><xsl:value-of select="@id"/></td>
         <td><xsl:if test="@naimen = ''"><xsl:attribute name="bgcolor">#ff0000</xsl:attribute></xsl:if><xsl:value-of select="@naimen"/></td>
         <td><xsl:if test="@obozn = ''"><xsl:attribute name="bgcolor">#ff0000</xsl:attribute></xsl:if><xsl:value-of select="@obozn"/></td>
         <xsl:if test="$isMid3 = 'yes'">
            <td><xsl:if test="@gost = ''"><xsl:attribute name="bgcolor">#ff0000</xsl:attribute></xsl:if><xsl:value-of select="@gost"/></td>
			<td><xsl:if test="@marka = ''"><xsl:attribute name="bgcolor">#ff0000</xsl:attribute></xsl:if><xsl:value-of select="@marka"/></td>
         </xsl:if>
         <td><xsl:if test="@num_kol = '0'"><xsl:attribute name="bgcolor">#ff0000</xsl:attribute></xsl:if><xsl:value-of select="@num_kol"/> <xsl:value-of select="concat(' ',@kei)"/>.</td>
	 <xsl:variable name="col" select="user:getColor(number(../@code))"/>
    	 <td>
	    <font><xsl:attribute name="color"><xsl:value-of select="$col"/></xsl:attribute>
	      <xsl:value-of select="../@obozn"/><xsl:if test="../@num_kol != '1'">[<xsl:value-of select="../@num_kol"/>]</xsl:if>
	    </font>
	 </td>
      </tr>
      <tr class="collapsed" bgcolor="#ffffff"><xsl:attribute name="id">src<xsl:value-of select="$source-id"/></xsl:attribute>
    	 <td colspan="7">
    	  <table width="97%" border="1" bordercolor="#dcdcdc" rules="cols" class="issuetable">				
				<xsl:if test="../@code = 0">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:apply-templates select=".."/></td></tr>
				</xsl:if>
				<xsl:if test="@code = 1">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:apply-templates select=".."/></td></tr>
				</xsl:if>
				<xsl:if test="@code = 2">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:apply-templates select=".."/></td></tr>
				</xsl:if>
				<xsl:if test="../@code = 3">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:apply-templates select=".."/></td></tr>
				</xsl:if>
				<xsl:if test="../@code = 4">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:apply-templates select=".."/></td></tr>
				</xsl:if>
				<xsl:if test="../@code = 7">
				<tr><td colspan="7" class="issuetitle">Вхождения: <xsl:value-of select="../@naimen"/></td></tr>
				</xsl:if>
				<tr><td colspan="7" class="issuetitle">Сумма: <xsl:value-of select="@sum"/> <xsl:value-of select="concat(' ',@kei)"/>.</td></tr>
				<xsl:if test="$isMid3 = 'yes'">
                <tr>
                    <td colspan="7" class="issuetitle">Раздел: <xsl:value-of select="./mid2/@naimen"/> [<xsl:value-of select="./mid2/@gost"/>] -> <xsl:value-of select="./mid2/mid1/@naimen"/> -> <xsl:value-of select="./mid2/mid1/mid0/@naimen"/></td>
                </tr>
				</xsl:if>
    	  </table>
    	 </td>
    	</tr>
        <xsl:if test="last() = position()"><xsl:variable name="foot_style" select="user:get_foot_class(1)"/>
            <tr valign="top">
    			<td colspan="7"><xsl:attribute name="class"><xsl:value-of select="$foot_style"/></xsl:attribute>
                Количество: <xsl:value-of select="user:get_count()"/>. 
                Общая сумма: <xsl:value-of select="user:get_sum()"/> <xsl:value-of select="concat(' ',@kei)"/>.
                </td>
            </tr>
        </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
