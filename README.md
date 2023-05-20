# README
May 2023  
Ruben Bernardino

**The proposed solution to the use case is presented in the `Test` project and its corresponding `Test.Tests` project. The relevant classes are `WordFrequencyAnalyzerStringProvider` and `WordFrequencyAnalyzerRegexProvider`.**

**Additionally, a showcase project `Api` and a Benchmark project `Test.Benchmark` are included and do not interfere with the proposed use case solution.**

## Word Analyzer Library

The `Test` project hold the word analyzer library.

Two approaches were implemented to address the problem:

- a `regular expressions` based
- a `string manipulation` based

As it will be explained in the [api showcase section](#api-showcase), separate interfaces were created to facilitate showcases (i.e. so one or the other could be injected without the need to change configuration).

The `IWordFrequencyFactory` was introduced to enable modification of `IWordFrequency` implementations without impacting the several `IWordFrequencyAnalyzer` implementations.

A `Test.Benchmark` project and a set of results from a comparison between Analyzers were also included but are not necessary to respond to the problem.

## API Showcase

Although the mentioned Libraries would suffice to solve the Test, a showcase through the `Api` project was composed.

To expose the functionality offered by `IWordFrequencyAnalyzer`, 3 example approaches are presented:

1. `WordFrequenciesController` - A simple approach where the logic is executed mostly at the `Controller` level.
2. `RegexAnalyzersController` - A CQRS based approach where only the `regular expressions` based functionality is exposed.
3. `StringAnalyzersController` - A CQRS based approach where only the `string manipulation` based functionality is exposed.

The [discussion section](#discussion) expands on thoughts about each of the approaches.

**Remarks:**

- A `token` endpoint is presented which will generate dummy JWT tokens. The expiration time (currently set to 30 minutes) can be changed through `appsettings.Development.json`.
- Apart from the `IdentityController`'s `token` endpoint, all others require a Bearer token, which can be set through the Swagger page (just directly paste the response from the `token` endpoint).
- For all of the approaches, a Contract was defined for the request objects. For simplicity sake, response objects were not defined.
- The CQRS based approaches leverage on a Mapper Library to translate the Contract request objects into CQRS request objects.
- Apart from the Validation provided out of the box by ASP.NET CORE, Validation at the CQRS level is configured for the `RegexAnalyzersController` request handling. However, it is not configured for the `StringAnalyzersController` nor `WordFrequenciesController` to enable comparison. One can see it in action by using the `/api/v1/regexanalyzers/cqrs/wordfrequencies/words` endpoint from `RegexAnalyzersController` while providing the **value `0` as the `numberOfWords` parameter**. It is possible to compare the behaviours by executing the same experiment on the `/api/v1/stringanalyzers/cqrs/wordfrequencies/words` endpoint from `StringAnalyzersController` instead.
- An example of exception handling other than CQRS Validation is presented through `InvalidWordExceptionFilter` which intercepts `InvalidWordException`. To see it in action, one can use the `/api/v1/stringanalyzers/cqrs/wordfrequencies/word` endpoint from `StringAnalyzersController` while providing an **empty string as the `word` parameter**. As previously mentioned, this controller does not benefit from CQRS Validation.
- A `swagger.json` endpoint where the API definition and JSON Schemas are presented is available. There is a link in the main Swagger page below the title.
- Versioning is exemplified in `WordFrequenciesController` and can be navigated through the Swagger page definitions.
- `WordFrequency` is an example of a `ValueObject`.

## Discussion

It is important to mention that Command Query Responsibility Segregation (CQRS) does not add much value to the use case. It is typically considered when is advantageous to separate read operations from write operations, especially when interacting with databases, which is not the case. This implementation only really holds the purpose of demonstration.

In order to comply with the requirement:

> *The source code file(s) delivered should be compilable once inserted into a project 
containing the interface defined in the same namespace (i.e. into the one we have waiting 
for the code already)*

The relevant interfaces `IWordFrequency` and `IWordFrequencyAnalyzer` and corresponding implementations had to be defined in the `Test` project, allowing it to be used at discretion as a single package.

Regarding DDD, the use case is simple enough to make the use of a `Domain` definition unjustified. Nothing in the context of the use case stands out as an `Entity`. However, `WordFrequency` is presented as an example of a `ValueObject`. 

A Clean Architecture showcase was considered and would have been implemented if it were not for the mentioned requirement, and the simplicity of the use case. To achieve it, one could consider a scenario where `IWordFrequency` and its implementation are defined in a `Domain` layer. The `IWordFrequencyAnalyzer` and the CQRS handlers would be defined in a `Application` layer. The implementation of `IWordFrequencyAnalyzer` would be a concern of a `Infrastructure` layer.

Looking at the current solution structure and based on the previous statements, `Test` could be spread throughout `Domain`, `Application` and `Infrastructure` and the chain of dependencies would be defined as:

`Api <--depends on-- Infrastructure <--depends on-- Application <--depends on-- Domain`

Hence, complying with Clean Architecture.

## Some showcase ideas that were considered but not implemented

- Claims based authorization
- Http Clients configured for specific external sources
- Assuming the Http Clients exist, Retry and Circuit-Breaker mechanisms
- Custom Middleware
- Structured Logging
- Background Services
- Messaging
