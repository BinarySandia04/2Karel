#!/usr/bin/env sh
echo "SandiGit > Pulled"
git add *
echo "SandiGit > Added"
git commit -m $0
echo "SandiGit > Commited with message $0"
git push origin master
echo "SandiGit > Pushed!"
