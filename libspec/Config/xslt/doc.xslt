<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:msxsl="urn:schemas-microsoft-com:xslt"
      xmlns:user="my_scripts">
<msxsl:script language="JScript" implements-prefix="user">
<![CDATA[ 
  var curr_item, curr_style;
  var count = 0;
  var sum = 0;
  curr_style = "content";
  function add(kol) {
		sum += kol;
		count++;
		return sum;
   }
   function get_class() {
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
        <title>table mid3</title>
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
	    if (check.lastIndexOf("plus") != -1){
	      uMe.src = "..\..\..\confg\css\minus.gif"
	    }
	    else{
	      uMe.src = "..\..\..\confg\css\plus.gif"
	    }
          }
        </script>
	</head>
          <body topmargin="0" leftmargin="0" rightmargin="0" onclick="outliner()">
            <h1>Лицевые чертежи</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//lid">
                <xsl:sort select="@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Обезличенные чертежи</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did//oid">
                <xsl:sort select="@obozn"/>
              </xsl:apply-templates>
            </table>
            <h1>Исходные чертежи</h1>
            <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
              <tr>
                <td nowrap="1" class="header">ID</td>
                <td nowrap="1" class="header">Наименование</td>
                <td nowrap="1" class="header">Обозначение</td>
                <td nowrap="1" class="header">Количество</td>
				<td nowrap="1" class="header">Документ</td>
              </tr>
              <xsl:apply-templates select="//did">
                <!--<xsl:sort select="@obozn"/>-->
              </xsl:apply-templates>
            </table>                                          
          </body>
        
      </html>

    </xsl:template>

  <!--<xsl:template match="gid">-->
  <!--  <xsl:value-of select="concat(' ',@id)"/>-->
  <!--</xsl:template>-->

  <xsl:template match="did">
    <xsl:call-template name="common">
      <xsl:with-param name="isDid" select="'yes'"/>
    </xsl:call-template>
  </xsl:template>
  <xsl:template match="oid">
    <xsl:call-template name="common">
      <xsl:with-param name="isDid" select="'no'"/>
    </xsl:call-template>
  </xsl:template>
  <xsl:template match="lid">
    <xsl:call-template name="common">
      <xsl:with-param name="isDid" select="'no'"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="parents">
    <xsl:for-each select="ancestor::*">
      <xsl:variable name="col" select="user:getColor(number(@code))"/>
      <font><xsl:attribute name="color"><xsl:value-of select="$col"/></xsl:attribute>
       <xsl:value-of select="@naimen"/> [<xsl:value-of select="@obozn"/>]
      </font>
    </xsl:for-each>
  </xsl:template>
  
  <xsl:template name="common">
      <xsl:param name="isDid"/>
      <xsl:variable name="test" select="user:test(concat('',@obozn))"/>
      <xsl:variable name="source-id" select="generate-id(.)"/>
      <xsl:if test="$test = 0">
    	<tr valign="top">
	    <xsl:variable name="foot_style" select="user:get_foot_class()"/>
    	    <td colspan="7"><xsl:attribute name="class"><xsl:value-of select="$foot_style"/></xsl:attribute>
            Количество: <xsl:value-of select="user:get_count()"/>. 
            Общая сумма: <xsl:value-of select="user:get_sum()"/>.
            </td>
    	</tr>
      </xsl:if>
	  <xsl:variable name="pos" select="count(descendant::*)"/>
      <xsl:variable name="sum" select="user:add(number(@sum))"/>
      <xsl:variable name="curr_style" select="user:get_class()"/>

	<tr><xsl:attribute name="class"><xsl:value-of select="$curr_style"/></xsl:attribute>
        <td><img border="0" alt="развернуть/свернуть секцию" height="11" onclick="changepic()" src="..\..\..\confg\css\plus.gif" width="9"><xsl:attribute name="child">src<xsl:value-of select="$source-id"/></xsl:attribute></img><xsl:value-of select="@id"/></td>
		<xsl:choose>
		<xsl:when test="$pos = 0">
			 <td bgcolor="#00ff00"><xsl:value-of select="concat('(???)',@naimen)"/></td>
		</xsl:when>
		<xsl:otherwise>
		 <td><xsl:value-of select="@naimen"/></td>
		</xsl:otherwise>
	  </xsl:choose>
       
        <td><xsl:value-of select="@obozn"/></td>
        <td><xsl:value-of select="@num_kol"/></td>
		<td>
			<font><xsl:attribute name="color"><xsl:value-of select="user:getColor(number(../@code))"/></xsl:attribute>
				<xsl:value-of select="../@obozn"/><xsl:if test="../@num_kol != '1'">[<xsl:value-of select="../@num_kol"/>]</xsl:if>
			</font>
		</td>
      </tr>
      <xsl:if test="$isDid = 'no'">
	<tr class="collapsed" bgcolor="#ffffff"><xsl:attribute name="id">src<xsl:value-of select="$source-id"/></xsl:attribute>
    	 <td colspan="7">
    	  <table width="97%" border="1" bordercolor="#dcdcdc" rules="cols" class="issuetable">
	    <tr><td colspan="7" class="issuetitle">Вхождения: <xsl:call-template name="parents"/><!--<xsl:apply-templates select=".."/>--></td></tr>
	    <tr><td colspan="7" class="issuetitle">Сумма: <xsl:value-of select="@sum"/>.</td></tr>
    	  </table>
    	 </td>
	</tr>
      </xsl:if>
        <xsl:if test="last() = position()"><xsl:variable name="foot_style" select="user:get_foot_class(1)"/>
            <tr valign="top">
    	      <td colspan="7"><xsl:attribute name="class"><xsl:value-of select="$foot_style"/></xsl:attribute>
                Количество: <xsl:value-of select="user:get_count()"/>. 
                Общая сумма: <xsl:value-of select="user:get_sum()"/>.
              </td>
            </tr>
        </xsl:if>
  </xsl:template>
  
</xsl:stylesheet>
