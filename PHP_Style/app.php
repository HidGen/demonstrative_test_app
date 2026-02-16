<?php
$input = readline('Введите данные: ');
$arData = explode(" ", $input);
$com = $arData[0];
$path = $arData[1];

switch ($com) {
    case 111:
        $output = shell_exec('dir /B "'.$path.'"');
        print_r($output);
        break;
    case 222:
        $arPath = explode("\\", $path);
        $fileName = $arPath[count($arPath) - 1];
        unset($arPath[count($arPath) - 1]);
        $content = file_get_contents($path);
        $up_content = strtoupper($content);

        $arFN = explode(".", $fileName);
        $newFileName = $arFN[0] . "_COMPLETE." . $arFN[1];
        $newPath = implode("/", $arPath)."/".$newFileName;
        file_put_contents($newPath, $up_content);
        print_r('Обработка завершена.');
        break;
}
