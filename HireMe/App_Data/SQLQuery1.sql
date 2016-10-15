use HireMe;

if (OBJECT_ID('App') is not null)
drop table App;

if (OBJECT_ID('Candidate') is not null)
drop table Candidate;

if (OBJECT_ID('Job') is not null)
drop table Job;

if (OBJECT_ID('Recruiter') is not null)
drop table Recruiter;
go

create table Recruiter 
(RecruiterID int identity(100,1) primary key,
RecruiterCompany varchar(20) not null,
RecruiterLastName varchar(20) not null);
go

create table Job
(JobID int identity(100,1) primary key,
JobTitle varchar(20) not null,
Jobdescription varchar(100) not null,
JobReqSkill varchar(100) not null,
JobCity varchar(15) not null,
JobSalaryRange varchar(20) not null, 
RecruiterID int not null,
constraint fkJobToRecruiter foreign key(RecruiterID)
references Recruiter(RecruiterID));
go

create table Candidate
(CSSN int identity (100,1) primary key,
CFname varchar(15),
CLName varchar(20));
go

create table App
(ACRN int identity (100,1) primary key,
AName varchar(20) not null,
ACity varchar(20) not null,
AState varchar(20) not null,
ASkills varchar(20) not null,
AEducation varchar(20) not null,
Areason varchar(100) not null,
JobID int not null,
constraint fkApplicationToJob foreign key(JobID)
references Job(JobID),
CSSN int not null,
constraint fkCandidateToApp foreign key(CSSN)
references Candidate(CSSN));
go
