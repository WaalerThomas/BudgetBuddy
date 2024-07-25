# Current plan
- The setup of the whole project is a mess. There needs to be some clean up here. The attempt of abstracting the core code from the persistent storage solution and the GUI implementation has made the whole thing complicated and bloated.
- Find out the business code of the project
    - Make some use-cases/requirements, and construct some unit tests for them. (Basically starting with test-driven development)

# Requirements
Format
```
The [user class or actor name] shall be able to [do something] [to some object] [qualifying conditions, response time, or quality statement].
```
User-Story format
```
As a [type of user]
I want [to do something]
So that [I achieve something]
```

## Functional
- [ ] A user should be able to create a new account by giving a name.
- [ ] A user should not be able to create an account with an empty name.
- [ ] A user should not be able to create accounts with the same name.

## Non-Functional
* ...