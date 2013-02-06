$global:psakeSwitches = '-docs', '-task', '-properties', '-parameters'

function script:psakeSwitches($filter) {
  $psakeSwitches | where { $_ -like "$filter*" }
}

function script:psakeDocs($filter) {
  psake -docs | out-string -Stream |% { if ($_ -match "^[^ ]*") { $matches[0]} } |? { $_ -ne "Name" -and $_ -ne "----" -and $_ -like "$filter*" }
}

function PsakeTabExpansion($lastBlock) {
  switch -regex ($lastBlock) {
    '(invoke-psake|psake) .* ?\-t[^ ]* (\S*)$' {
      psakeDocs $matches[2]
    }
    '(invoke-psake|psake) .* ?(\-\S*)$' {
      psakeSwitches $matches[2]
    }
    '(invoke-psake|psake) (\S*)$' {
      ls $matches[2]*.ps1 |% { "./$_" }
    }
  }
}
