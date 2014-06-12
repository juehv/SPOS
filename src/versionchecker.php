<?php
$aktuelle_version="0.1.0.6";
$version=$_GET["v"];
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>JUehV - Tech: Ihr Partner im IT Bereich</title>
</head>
<body>
<?php
if ($aktuelle_version==$version){
echo "<H2>Ihre Version ist aktuell!</H2>";
} else {
echo "<H2>Ihre Version ist NICHT aktuell!</H2><br/>
<p>Ihre Version: $version</p>
<p>Neueste Version: $aktuelle_version</p>
<p>Aktualisieren Sie Ihre Version: 
<a href=\"http://juehv-tech.de/spos/publish.htm\">download</a>";
}
?>
</body>
</html>