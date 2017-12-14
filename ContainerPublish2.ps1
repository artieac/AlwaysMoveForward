docker tag $args artieac/alwaysmoveforward.oauth2.web:OAuth2
docker push artieac/alwaysmoveforward.oauth2.web
docker logout
Invoke-Expression -Command (aws ecr get-login --no-include-email)
docker tag $args "215561421394.dkr.ecr.us-east-2.amazonaws.com/alwaysmoveforward/oauth2"
docker push "215561421394.dkr.ecr.us-east-2.amazonaws.com/alwaysmoveforward/oauth2"

