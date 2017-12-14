docker tag %1 artieac/alwaysmoveforward.oauth2.web:OAuth2
docker push artieac/alwaysmoveforward.oauth2.web
docker logout
powershell "Invoke-Expression" "-Command" "(aws ecr get-login)"
docker tag %1 215561421394.dkr.ecr.us-.dkr.ecr.us-east-2.amazonaws.com/alwaysmoveforward.oauth2.web
docker push 215561421394.dkr.ecr.us-.dkr.ecr.us-east-2.amazonaws.com/alwaysmoveforward.oauth2.web