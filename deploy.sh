#!/bin/bash -e

environment="Development"
tag=""
lifecycle="forever"
serviceName="alwaysmoveforward.oauth2.web"
dnsName="security.oauth2.alwaysmoveforward.com"
destinationCluster="prod-amf-us-v1"
internalLoadBalancer=true
baseServiceName="alwaysmoveforward.oauth2.web"
branchName="master"
failoverDeployment=false
valuesFile="dev-values.yaml"

while test $# -gt 0; do
    case "$1" in
        -h|--help)
            echo "options:"
            echo "-h, --help                            show brief help"
            echo "-e, --environment=ENVIRONMENT         environment to use: Development,Production" 
            echo "-t, --tag=TAG                         image tag to use"
            echo "-s, --servicename=SERVICE_NAME        service name to use"
            echo "-b  --branchname=BRANCH_NAME          branch to tag this deployment with"
            echo "-d, --dnsname=DNS_NAME                dns name to use"
            echo "-f, --failoverDeployment=true         deploy to the failover cluster in eu-west London (production only)"
            exit 0
            ;;
        -e|--environment)
            shift
            environment=$1
            shift
            ;;
        -t|--tag)
            shift
            tag=$1
            shift
            ;;
        -s|--servicename)
            shift
            serviceName=$1
            shift
            ;;
        -b|--branchname)
            shift
            branchName=$1
            shift
            ;;
        -d|--dnsname)
            shift
            dnsName=$1
            shift
            ;;
		-f|--failover)
            shift
            failoverDeployment=$1
            shift
            ;;
        *)
            break
            ;;
    esac
done

if [ -z $tag ]; then
    echo "tag is required"
    exit 1
fi

if [ $environment = "Production" ]; then
	destinationCluster="amf-aws-us-east-2.clusters.vpsvc.com"
	valuesFile="prod-values-aws-us-east-2.yaml"
fi

if [ $baseServiceName = $serviceName ]; then
    lifecycle="forever"
else
    lifecycle="temporary"
fi

echo "Deploying using helm to: "
echo "Environment: $environment"
echo "Tag: $tag"
echo "Lifecycle (temporary deployments should be cleaned up once it is no longer used): $lifecycle"
echo "Base Service Name: $baseServiceName"
echo "Branch Name: $branchName"
echo "Helm name (base + branch (or just base if branch is master)): $serviceName"
echo "DNS name: $dnsName"
echo "Is Failover?: $failoverDeployment"
echo "Cluster: $destinationCluster"
echo "Zipkin Service Name: $serviceName"

helm --kube-context=$destinationCluster upgrade $serviceName ./charts/alwaysmoveforward.oauth2.web -f ./charts/alwaysmoveforward.oauth2.web/$valuesFile --install \
--set nameOverride=$serviceName \
--set environment=$environment \
--set service.internalLoadBalancer=$internalLoadBalancer \
--set service.dnsName=$dnsName \
--set image.lifecycle=$lifecycle \
--set image.branchName=$branchName \
--set image.base=$baseServiceName \
--set image.tag=$tag \
--set zipkin.serviceName=$serviceName # --dry-run --debug