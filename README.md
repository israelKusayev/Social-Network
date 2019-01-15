## server setup:
the server must include the following software: <br/>
##### -IIS with 5 sites: <br/>
&ensp;	socialauth.com <br/>
&ensp;	SocialIdentity.com <br/>
&ensp;	SocialSocial.com <br/>
&ensp;	SocialNotifications.com <br/>
&ensp;	socialUi.com (defult site for binding) <br/>
-UrlReWrite for iis <br/>
-node js <br/>
-neo4j <br/>
-.net frameworks 4.7.1 <br/>
## other requirments
-server must have previliges to put files on s3 <br/>
-server must have previliges to put files on dynamo db <br/>
-server must have web and neo4j ports open <br/>
##### -dynamoDb with the following tables: <br/>
&ensp;	Auth  - hash key "UserName" of type string <br/>
&ensp;	BlockUsers - hash key "UserId" of type string <br/>
&ensp;	OAuth - hash key "FaceBookId" of type string <br/>
&ensp;	Tokens - hash key "UserId" of type string range key "TimeStamp" of type numeric <br/>
&ensp;	Users - hash key "UserId" of type string <br/>
## usage requirments:
in order to use the deployed app properly the "hosts" file must be update. <br/>
the updated file must include redirecting the following address to the relevant server ip: <br/>
socialauth.com <br/>
SocialIdentity.com <br/>
SocialSocial.com <br/>
SocialNotifications.com <br/>

alternative opstions are to use dedicated servers for each server (and modifing  config files) ,
using public DNSs or using difrent ports
