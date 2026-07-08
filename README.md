# About this project
A CLI Tool (CommandLine Interface) for generating, comparing and saving SHA256 Hash Code.
# What is SHA256?!
SHA-256 (Secure Hash Algorithm 256-bit) is a cryptographic hash function that takes an input and produces a fixed-size 256-bit (32-byte) output, typically represented as a 64-character hexadecimal string.
# Where can we use them?
- Bitcoin and blockchain technology
- Password hashing (often with salting)
- File integrity verification (checksums)
- Digital signatures and certificates
- Data fingerprinting
- and so on.

  -------

# How does this projct work?
Well it's so easy to work with this application. As I mentioned, it's a CommandLine Interface and you should work with it with cmd on windows.
Before we get inside of it, it's better for you to add the executable format (.exe) to the **Environment path** on **windows**. You can put any name on it. The default name is SHA256.exe<br>
There are lots of tutorials that teach how we can do this. It's better to take a look of them.
<br>
Any way, here we learn how this app works:<br>
![A view of application](/Assets/Images/AViewOfApp.PNG)<br>
As you can see, I wrote the basic usage of how can we work with it.<br>
But here I try to explain it with more details.
<br>
At first, this app opens files as stream. It means you can generate hash code from large file (more that 50 GB), but it takes time, you know!<br>
This app reads files byte by byte!<br>
Now there we are!<br>
The simplest way that we use all the time is hashing file for downloading, I mean, when we download a file from internet,<br> they provide the file with a SHA-256 code, something like **5f82577456dd4637fc4f79350cc8a1db4b8505a9d5b22c73a75aef18e34efeb7**!<br>
What is it used for?<br>
We use hash codes to be sure we downloaded a file completely.<br>
It means we use it just for downloading? Ofcource not!<br>
We use it **Everywhere** I mentioned that before.<br>
In this app, we can generate hash code from files.<br>
The simplest option we can use here is **-f**<br>
**-f** means generate hash code from specified file.<br>
Example: <br>
![Example of -f](/Assets/Images/Option-f.PNG)<br>
As you can see it generated hash code.
You can save the hash code to a **.xml** file too with option **-s**
Here are the exmaple:<br>
![Example of -f with -s](/Assets/Images/Option-f_with-s.PNG)<br>
Notice: if you use **-s** option with a **specified path**, app doesn't display the hash code on screen!<br>
It will save the hash code to a file called **GeneratedHash.xml**, this option **doesn't generate the folder it should generated befor**<br>
Option **-s** is optional, you can use it or not, it's your choice.<br>
The next option we can use is **d**, option **-d** means search a **directory** and **subfolders** and **all files** (Seaching full directory)<br>
and then generate hash codes from all files.<br>
It's better to use option **-s** with it because maybe the directory you choose has more that 300 files, and if you don't use **-s**,<br>
it will print all of them on the screen(it makes the screen ugly, you know!)<br>
The example of using **-d** with **-s**:<br>
![Example of -d with -s](/Assets/Images/Option-d_with-s.PNG)<br>
OK, now we're gonna see the option **-c**, Comparing...<br>
I know comparing is so cool the thing that all of us want.<br>
This option can take two actions:<br>
- **1** The simplest: Compare two hashes<br>
Example:<br>
![Example of -c Simple](/Assets/Images/Option-c_Simple.PNG)<br>
- **2** The hardest(somehow): Compare two **GeneratedHash.xml**<br>
Example:<br>
![Example of -c Hard](/Assets/Images/Option-c_Hard.PNG)<br>
The comparing hash codes with two **GeneratedHash.xml** works simple.<br>
At first, it counts the **Element File**<br>
Example:<br>
![View Of GeneratedHash](/Assets/Images/ViewOfGeneratedHash.PNG)<br>
As you can see, I highlighted them with yellow, the application count all of the **File Element** from first and second **GeneratedHash.xml**,<br>
If they were the same, it will compare **Element by Element** from first to last.<br>
Example of comparing two **GeneratedHash.xml** (I edited some values to print the incorrect files for testing:<br>
![Example of Comparing with Incorrect files](/Assets/Images/ComparingWithIncorroctFiles.PNG)<br>
Here we undestand how many errors we got, and which files are incorrect.
<br><br>
Some tips befor using my app<br>
**1** - Please don't edit the **GeneratedHash.xml** files. It's so important!<br>
**2** - If you wanna checksum files, at first copy them from source to your destination, then generate hash codes from source and destination, Then compare them. <br>Don't copy them and change file name and generate hash codes, the comparing works with file name and hash codes.<br>
**3** - And please if you got error, feel free to tell me, I'm here to listen and help, but I'm talking about real errors, not simplest OK!<br>

# Where we use this app?<br>
I use my app for generating hash codes and checksums of files especially videos<br>
When I copy some videos from source to a destination, I checksum and compare them<br><br>

 -------

 I hope you enjoy my application...<br>
 That's it.Thanks for reading.<br><br><br><br><br>




 Just for NB!
