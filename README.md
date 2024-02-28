## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# _das-learner-verification-api_

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-learner-verification-api?branchName=main)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3182&branchName=main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-learner-verification-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=SkillsFundingAgency_das-learner-verification-api)

```
This repository abstracts calls to perform learner verification (such as verifying ULN, Name and DOB).
```

## How It Works

```
The Learner Verification Inner API connects to the LRS Web Service to allow the following operations:
- verify - Verifies the existence of a learner using ULN, first name, last name and also optionally, gender and date of birth
```

## 🚀 Installation

### Pre-Requisites

```
* A clone of this repository
* A code editor that supports .NET 6
* An Azure Table Storage emulator
* In order to successfully access the certificate from the Key Vault, DevOps team must ensure that your AAD account is given the "Key Vault Certificates Officer" and "Key Vault Secrets Officer" role on the key vault das-at-lrnrvrf-kv. Your IP address must also be whitelisted on the key vault's firewall.
```
### Config

```
This repository uses the standard Apprenticeship Service configuration. All configuration can be found in the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config).

- A URL for the key vault resource created solely for this API
- The settings required for connecting to the LRS WCF Web Service:
   - The name of the digital certificate for sending authenticated requests
   - The base URL
   - The credentials required for sending authenticated requests (Organisation Reference, User Name, Organisation Password)
```

Azure Table Storage config

Row Key: SFA.DAS.LearnerVerification.Api_1.0

Partition Key: LOCAL

Data: 
_***Populate JSON below using config for AT environment from das-employer-config_

```json
{
  "ApplicationSettings": {
    "LearnerVerificationKeyVaultUrl": "***",
    "LrsApiWcfSettings": {
      "LRSCertificateName": "***",
      "LearnerServiceBaseUrl": "***",
      "OrganisationRef": "***",
      "OrgPassword": "***",
      "UserName": "***"
    }
  }
}
```

## 🔗 External Dependencies


```
None
```

## Technologies

```
* .NET 6
* Azure Table Storage
* WCF Web Service Reference
* NUnit
* Moq
* AutoFixture
* FluentAssertions
* Swagger / Swashbuckle
```

## 🐛 Known Issues

```
None
```