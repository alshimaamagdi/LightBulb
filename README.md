# LightBulb
How I build Thos Project
FirstStep Do Authorization With JWT   

1-install 
*EntityFrameworkCore.Authentication.JwtBearer
*EntityFrameworkcore.entityFrameworkCore
*EntityFrameworkcore.entityFrameworkCore.Design
*EntityFrameworkcore.entityFrameworkCore.sqlserver
*EntityFrameworkcore.visualstudio.web.codegenerated.design
*EntityFrameworkcore.identityModel.Tokens.jwt

2-IN AppsettingJsonFile
add   "JWT": {
    "Key": "S/BXt+FPnrEEqyyzP0sVjCIWKrO1uIgg1a86j9vqhPM=",
    "Issuer": "security",
    "Audience": "User",
    "DurationInDays": 30

  }

