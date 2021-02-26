# KAMAR Calendar

### Not affiliated with KAMAR Limited

KAMAR Calendar allows you to have your school timetable show up in your calendar using an iCal subsciption.
It has been tested in Google Calendar and macOS Calendar, but other common calendar applications should work too.

# Usage

To use KAMAR Calendar you will need to create a url with your username and password to allow the app to access your info. Do not share this with anybody as otherwise they will get access to your password and all of your school acounts, including email and Google Drive.

Once you have this url, you will just add it as an iCal subscription.

## Mount Maunganui College Students

To create your url, copy and paste the url below into the address bar of a new tab. Then replace 'username' with your KAMAR username, and 'password' with your password.

```
https://kamar-calendar.herokuapp.com/ICal/_/username/password
```

You should end up with something that looks like this:

```
https://kamar-calendar.herokuapp.com/ICal/_/jamesbond/w@llaB13
```

&hellip;and the start of the page should look like this:

```
BEGIN:VCALENDAR
VERSION:2.0
```

## Students of other schools

While KAMAR Calendar was originally designed for use by student at Mount Maunganui College, there shouldn't be any reason why it won't work with KAMAR systems from other schools. The only change that you should need to make is replacing the underscore in the url `_` with the KAMAR Portal Address that you use in the KAMAR app.

# Why we need your password

In general, apps should not need to ask for a password for other apps. However, the KAMAR API, according to the [scarce public documentation](https://github.com/awahid101/API), does not provide an easy to use method for authenticating without giving this app your credentials. Also, the urls do not just contain the API Token because it is unclear how long these tokens last, and expiry of a token could lead to undetectable errors.

# Credits

A big thank you to Abdul Wahid for his [reverse engineering work](https://github.com/awahid101/API/blob/master/api.md). While I have never talked with him, he has released his research under an [MIT License](https://github.com/awahid101/API/blob/master/LICENSE), meaning that anyone can use it as long as they follow the terms of the license.

I, Benjamin Davies, as the author of this code, also release it under that same MIT License.
