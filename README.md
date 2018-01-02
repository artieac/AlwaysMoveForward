# AlwaysMoveForward
This is a common project for a few other GIthub projects that I play around with.  Specifically the AnotherBlog and PointChart ones.

The code base is .Net, and recently I added in some .Net Core in service to an OAuth2 Identity Service I started working with.

There are a few different folders/projects in here under src.  Those are the relevant ones a the moment (PointChart and TagTalk are in very much a work in progress and are also in the process of being relocated from here to somplace else).

OAuth - This is actually two different OAuth services
1. An OAuth 1.0a service implemented using DevDefineds OAuth libraries.  This was written using .Net 4.5;
2. A newer OAuth 2.0 service implemented using Identity Service 4 and .Net Core

Common - Some common classes like logging helpers, and configuration helpers.

Main Site - A basic set of pages that serves as the entry point to some of the other things I've deployed.

## Getting Started

Once you've cloned the source control.  Start by going to the Database folder and running the scripts in order, and setup an application user in the database.  This should create the database with the default data that is neccessary to run the app.

From there open the project in your Visual Studio, modify the configuration settings to match your database user id/pwd and you should be able to just run it.

### Prerequisites
The scripts are written for Sql Server and I use VS2018 as my IDE.  

## Running the tests

There are some unit tests created using NUnit, but there could always be some more.

## Deployment

This currently just builds the apps and there is a manual deployment to my hosting provider.  The only exception is the OAuth2 projects which I'm working on configuring to deploy to AWS.

## Built With

TBD

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

Not much versioning yet beyond what is in master.

## Authors

* **Arthur Correa** - *Initial work* - [AlwaysMoveForward.com](http://www.alwaysmoveforward.com)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments
