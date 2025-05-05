# Build Instructions
1. Clone the repo and cd into it <br>
2. You should see the a folder (WhoseTurn) and several other files <br>
3. Run the following command: docker build --rm -t whoseturn . <br>
4. Upon success this command: docker run --rm -p 9000:9000 -p 9001:9001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS="http://+:9000" whoseturn <br>
5. Upon success, navigate to http://localhost:9000/swagger/index.html <br>
6. You should be presented with a swagger page with two endpoints <br>
7. If all this fails, and you have the dotnet CLI and .NET 8 SDK installed, you can always run "dotnet run" on the same root directory you ran docker commands

**The database is pre-seeded with data. You can get get the current status of what it looks like by using GET /determinant**

# Assumptions
• The number of employees are static, there is no add or remove function, employees in the database are pulled from Bertram Labs Team paage <br>
• The company is static, querying is based on the request coming in via Id's <br>
• Not everybody goes out for coffee. Some people don't want coffee so they simply don't have to be included in the request <br>
• I would never put database connection details just like how it is in this repo, I'm assuming you guys arent hackers, but for the sake of easiness, I've left that information in plain sight on the appsettings. In the real world, these things would be handled with much more care 

# Approach
I went with a price weighted approach. Whoever is in the order will have their past "total amount paid" queried for and we'll subtract their order from the order total. This gives us what the person is paying for everyone else. I believe this to be most "fair", so that someone who orders a $35 coffee isn't skewing the order total.

# Shape of Data
The shape of the request is a an object of lists: <br>
```
{
  "persons": [
    {
      "id": 0, // INT
      "name": "string", // STRING
      "itemOrderedAmount": 0 // DECIMAL
    }
  ]
}
```
The easiest way would be to make a GET request to get the ids needed, and then fill in all the people who are getting coffee