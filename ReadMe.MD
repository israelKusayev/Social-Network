##server setup:
the server must include the following software:
-IIS with 5 sites:
	socialauth.com
	SocialIdentity.com
	SocialSocial.com
	SocialNotifications.com
	socialUi.com (defult site for binding)
-UrlReWrite for iis
-node js
-neo4j
-.net frameworks 4.7.1
##other requirments
-server must have previliges to put files on s3
-server must have previliges to put files on dynamo db
-server must have web and neo4j ports open
-dynamoDb with the following tables:
	Auth  - hash key "UserName" of type string
	BlockUsers - hash key "UserId" of type string
	OAuth - hash key "FaceBookId" of type string
	Tokens - hash key "UserId" of type string range key "TimeStamp" of type numeric
	Users - hash key "UserId" of type string
##usage requirments:
in order to use the deployed app properly the "hosts" file must be update.
the updated file must include redirecting the following address to the relevant server ip:
socialauth.com
SocialIdentity.com
SocialSocial.com
SocialNotifications.com

alternative opstions are to use dedicated servers for each server (and modifing  config files) ,
using public DNSs or using difrent ports