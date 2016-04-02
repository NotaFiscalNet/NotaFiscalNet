param([string]$buildFolder, [string]$version, [string]$email, [string]$username, [string]$personalAccessToken)

write-host "Iniciando build da documentação"
cd docs
dnu commands install docfx
docfx src\docfx.json

Write-Host "Configurando acesso ao git"
git config --global user.email $email
git config --global user.name $username
git config --global push.default matching
git config --global credential.helper store
Add-Content "$env:USERPROFILE\.git-credentials" "https://$($personalAccessToken):x-oauth-basic@github.com`n"

Write-Host "Clonando site"
cd "$($buildFolder)\..\"
mkdir gh-pages
git clone --quiet --branch=master https://github.com/NotaFiscalNet/NotaFiscalNet.github.io/ gh-pages
cd gh-pages
git status

Write-Host "Limpando projeto gh-pages"
Get-ChildItem -Attributes !r | Remove-Item -Recurse -Force

Write-Host "Conteudo nova documentação"
Copy-Item -path "$($buildFolder)\docs\src\_site\*" -Destination $pwd.Path -Recurse
git status

$thereAreChanges = git status | select-string -pattern "Changes not staged for commit:","Untracked files:" -simplematch

if ($thereAreChanges -ne $null) { 
    Write-host "Comitando alterações"
    git add --all
    git commit -m "Documentacao do site v.$($version)"
    git push --quiet
} 
else { 
    write-host "Sem alterações para commitar"
}