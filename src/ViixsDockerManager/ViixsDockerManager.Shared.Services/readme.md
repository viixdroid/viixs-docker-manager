# ViixDockerManager.Shared.Services

Contains interfaces to be shared between multiple projects. For example, creating a use account will be implemented by the user ddl, but is also needed by the setup step.
If so, this library will be the way to communciate. This makes sure that the setup library does not know how internals of creating users works.