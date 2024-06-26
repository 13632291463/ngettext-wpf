# Invoke this script fron the Nuget package manager console.  CD into the directory of this script before invoking.
# 在 Nuget 包管理器控制台上调用此脚本。 CD 添加到此脚本的目录中，然后再调用。

# gettext tools don't setup their path correctly yet, so here is a work-around
# getText 工具尚未正确设置其路径，因此这里有一个解决方法
$env:Path += ";..\packages\Gettext.Tools.0.19.8.1\tools\bin"

# Extract msgids from xaml files in project into pot file.  If you installed NGettext.Wpf via nuget you can source like so:
# 将项目中xaml文件中的msgids解压到pot文件中。 如果通过 nuget 安装了 NGettext.Wpf，则可以像这样进行源：
#   . XGetText-Xaml.ps1
# instead of the following
. ../XGetText.Xaml/XGetText-Xaml.ps1
XGetText-Xaml -o obj/xamlmessages.pot -k Gettext,GettextFormatConverter @(Get-ChildItem -Recurse -File -Filter *.xaml | Where { $_.FullName -NotLike '*\obj\*' } | ForEach-Object { $_.FullName })

Get-ChildItem -Recurse -File -Filter *.cs | Where { $_.FullName -NotLike '*\obj\*' } | ForEach-Object { $_.FullName } | Out-File -Encoding ascii "obj\csharpfiles"

# Extract msgids from cs files in project into pot file
# 将 msgids 从项目中的 cs 文件中提取到 pot 文件中
xgettext.exe --force-po --from-code UTF-8 --language=c# -o obj/csmessages.pot -k_ -kNoop:1g -kEnumMsgId:1g --keyword=Catalog.GetString --keyword=PluralGettext:2,3 --files-from=obj\csharpfiles

# Merge two pot files into one
# 将两个 pot 文件合并为一个
msgcat.exe --use-first -o obj/result.pot obj/csmessages.pot obj/xamlmessages.pot

# Update po files with most recent msgids
# 使用最新的 msgids 更新 po 文件
$locales = @("da-DK", "de-DE", "zh-CN")
$poFiles = $($locales | ForEach-Object { "Locale/" + $_ + "/LC_MESSAGES/Example.po" })

$poFiles | ForEach-Object {
	msgmerge.exe --sort-output --update $_ obj/result.pot 2> $null
}

echo "Po files updated with current msgIds: " $poFiles
echo “Po 文件更新了当前的 msgIds：”$poFiles
echo "You may now edit these files with PoEdit (https://poedit.net/)"
echo “您现在可以使用 PoEdit （https://poedit.net/） 编辑这些文件”