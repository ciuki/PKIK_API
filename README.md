<div id="top"></div>



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Status][status-shield]][status-url]
[![Contributors][contributors-shield]][contributors-url]
[![Commits][commits-shield]][commits-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">

  <h3 align="center">PKIK API</h3>

  <p align="center">
    ·
    <a href="https://github.com/ciuki/PKIK_API/issues">Report Bug</a>
    ·
    <a href="https://github.com/ciuki/PKIK_API/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li>
      <a href="#architecture">Architecture</a>
      <ul>
        <li><a href="#aproach">Aproach</a></li>
        <li><a href="#folder-structure">Folder Structure</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project


Project classes of the subject "Multimedia systems" at the Electrical Faculty of the Silesian University of Technology.


<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
* [.NET](https://docs.microsoft.com/pl-pl/dotnet/core/whats-new/dotnet-6)
* [Docker](https://www.docker.com/)
* [SQL Server](https://www.microsoft.com/en-us/sql-server)

#### Used NuGets

* [AutoMapper](https://automapper.org/)
* [coverlet.collector](https://github.com/coverlet-coverage/coverlet)
* [Faker.Net](https://github.com/oriches/faker-cs)
* [FluentAssertions](https://fluentassertions.com/)
* [FluentValidation](https://fluentvalidation.net/)
* [FluentValidation.AspNetCore](https://fluentvalidation.net/)
* [MicroElements.Swashbuckle.FluentValidation](https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation)
* [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://asp.net/)
* [Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://asp.net/)
* [Microsoft.AspNetCore.Mvc.Testing](https://asp.net/)
* [Microsoft.EntityFrameworkCore.InMemory](https://asp.net/)
* [Microsoft.EntityFrameworkCore.SqlServer](https://asp.net/)
* [Microsoft.NET.Test.Sdk](https://asp.net/)
* [Microsoft.VisualStudio.Azure.Containers.Tools.Targets](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/visual-studio-tools-for-docker?view=aspnetcore-2.1)
* [MimeKit](http://www.mimekit.net/)
* [Scrutor.AspNetCore](https://github.com/sefacan/Scrutor.AspNetCore)
* [Stubble.Core](https://github.com/stubbleorg/stubble)
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [Swashbuckle.AspNetCore.Annotations](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [Swashbuckle.AspNetCore.Filters](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [Swashbuckle.AspNetCore.Newtonsoft](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [Swashbuckle.AspNetCore.ReDoc](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [xunit](https://github.com/xunit/xunit)
* [xunit.runner.visualstudio](https://github.com/xunit/visualstudio.xunit)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

Docker or Visual Studio 2022

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/ciuki/PKIK_API.git
   ```
   #### For Docker
2.1 Generate image from Dockerfile
   ```sh
   docker build -f .\PKIK_API\PKIK_Project\Dockerfile -t smprojekt:image .\PKIK_Project_API\.
   ```
3.1 Run container from image
   ```sh
   docker run -p 80:80 -d --name PKIK_Project smprojekt:image
   ```
4.1 Now you should be able to go to [localhost](http://localhost/swagger)

   #### For Visual Studio 2022
   
2.2 Open project in Visual Studio 2022

3.2 If Docker is not working then change launchSetting to IIS Express

![Folder structure](https://github.com/ciuki/PKIK_API/blob/Development/ReadMeData/launch_settings.png?raw=true)

4.2 Application should automatically display swagger page

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

_For more examples, please refer to the [Documentation](https://smprojektapi.herokuapp.com/api-docs)_

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Add Users
- [x] Add Polls and Voting
- [x] Add in app and mail notifications
- [x] Add project documentation
- [x] Add unit tests
- [ ] Multi-language Support
    - [x] English
    - [ ] Polish
- [ ] Use factories for entities

See the [open issues](https://github.com/ciuki/PKIK_API/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS -->
## Architecture

### Aproach

Code was writen in code first aproach.
Project was made using DDD.

### Folder structure
Project uses clean architecture
It is divided into 4 layers:
API Layer(Application Layer(Infrastructure Layer(Core Layer)))
Child Layer does not know about existence of Parent Layer

![Folder structure](https://github.com/ciuki/PKIK_API/blob/Development/ReadMeData/folder_structure.png?raw=true)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- LICENSE -->
## License

Distributed under the GNU GPL License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/ciuki/PKIK_API?style=for-the-badge
[contributors-url]: https://github.com/ciuki/PKIK_API/graphs/contributors
[status-shield]: https://img.shields.io/website?down_color=red&down_message=offline&label=demo&style=for-the-badge&up_color=green&up_message=online&url=https%3A%2F%2Fsmprojekt.herokuapp.com
[status-url]: smprojekt.herokuapp.com
[commits-shield]: https://img.shields.io/github/commit-activity/m/ciuki/PKIK_API?style=for-the-badge
[commits-url]: https://github.com/ciuki/PKIK_API/pulse
[issues-shield]: https://img.shields.io/github/issues/ciuki/PKIK_API?style=for-the-badge
[issues-url]: https://github.com/ciuki/PKIK_API/issues
[license-shield]: https://img.shields.io/badge/license-GPL-blue?style=for-the-badge&logo=appveyor
[license-url]: https://github.com/ciuki/PKIK_API/blob/Development/ReadMeData/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/rafa%C5%82-miczek-096582227
[product-screenshot]: images/screenshot.png
