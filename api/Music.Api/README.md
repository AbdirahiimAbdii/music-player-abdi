## What should you do?
An application in which it is possible to add artists, music albums and tracks/songs to each album, these are correctly linked in the database and displayed correctly in the application.

## Stack
- Database - SQLite
- Backend: C#/.NET + any framework if you want (eg Entity Framework)
- Frontend: React + React Bootstrap + Bootstrap for styling
- Git - for version control (use GitHub as git server
- Work methodology - carefully follow the instructions under work methodology.

## The database
The database file should be included in the repo.
- Create a database that keeps track of which music albums have been released by which artist.
- Make sure that you can store the artist name, description of the artist, and that a unique ID is created per artist.
- Make sure that you can store the album name, the year the album was released, and that a unique ID is created per album, as well as which artist (artist ID) the album was released by.
- Make sure you can store which tracks/songs are on each album, as well as the order in which they appear on the album.

## The web application
- Build a web application with a frontend and a REST api.
- In the web application, there should be a page where all artists are listed on one page. If you click on an artist, a list of all the albums the artist has released should appear.
- There should be a page to add a new artist.
- There should be a page to add a new album (and link it to a specific artist).
- The web interface must be created using Bootstrap + React Bootstrap.
- The web interface must be responsive, i.e. adapt to and work equally well on mobiles, tablets and computer screens of different sizes.
- VG requirements: It must be possible to upload images of artists and albums which are then displayed in connection with the respective artist and album.

## REST tests in PostMan
- Set up a "test database" (a variant of the database that you know the exact contents of), so you know exactly what data to test against. (This database may get "broken"/affected during the tests. So make sure there is a copy of what it should look like BEFORE the tests are run, that you can recreate it from.)
- In the test database there must be at least 5 artists, at least 15 albums and at least 120 tracks/albums. All albums must be linked to an existing artist. All tracks must be linked to an existing album.
- Test with PostMan that all REST routes work (ie the 5 usual per table that should exist - GET that returns all data from a table, GET that returns a row based on id, POST that creates a row in a table, PUT that changes a row in the table, DELETE which removes a row in the table ).
- Test that responses are returned with the code 200 and that it takes a maximum of 500 ms to get a response.
- Test that the answers have correct content.
- Collect all your tests in a Collection and export it as a JSON file from PostMan.