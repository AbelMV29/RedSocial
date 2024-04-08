CREATE DATABASE RedSocial;

CREATE TABLE Identity_User(
	identity_user_id NVARCHAR(50) PRIMARY KEY NOT NULL,
	username NVARCHAR(50) UNIQUE NOT NULL,
	email NVARCHAR(50) UNIQUE NOT NULL,
	password_hash NVARCHAR(50) NOT NULL
);

CREATE TABLE Profile_User(
	profile_user_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	name NVARCHAR(50) NOT NULL,
	last_name NVARCHAR(50) NULL,
	photo NVARCHAR(MAX) NULL,
	identity_user_id NVARCHAR(50) FOREIGN KEY REFERENCES Identity_User(identity_user_id)
);

CREATE TABLE Request(
	profile_user_id INT FOREIGN KEY REFERENCES Profile_User(profile_user_id) NOT NULL,
	friend_id INT FOREIGN KEY REFERENCES Profile_User(profile_user_id) NOT NULL,
	date_request DATETIME NOT NULL,
	state_request BIT NOT NULL,
	PRIMARY KEY(profile_user_id,friend_id)
);

CREATE TABLE Post(
	post_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	title NVARCHAR(50) NOT NULL,
	body NVARCHAR(MAX) NOT NULL,
	photo NVARCHAR(50) NULL,
	date_publish DATETIME NOT NULL,
	profile_user_id INT FOREIGN KEY REFERENCES Profile_User(profile_user_id) NOT NULL
);

CREATE TABLE Category(
	category_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	name NVARCHAR(50) NOT NULL
);

CREATE TABLE Post_Category(
	post_id INT FOREIGN KEY REFERENCES Post(post_id) NOT NULL,
	category_id INT FOREIGN KEY REFERENCES Category(category_id) NOT NULL,
	PRIMARY KEY(post_id,category_id)

);

CREATE TABLE Comment(
	comment_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	body NVARCHAR(MAX) NOT NULL,
	photo NVARCHAR(MAX) NULL
);

CREATE TABLE Comment_Post_Profile(
	comment_id INT FOREIGN KEY REFERENCES Comment(comment_id) NOT NULL,
	post_id INT FOREIGN KEY REFERENCES Post(post_id) NOT NULL,
	profile_id INT FOREIGN KEY REFERENCES Profile_User(profile_user_id) NOT NULL,
	date_publish DATETIME NOT NULL,
	PRIMARY KEY(comment_id,post_id)
);

CREATE TABLE Response(
	response_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	body NVARCHAR(MAX) NOT NULL,
	photo NVARCHAR(MAX) NULL,
	response_parent_id INT FOREIGN KEY REFERENCES Response(response_id) NULL
);

CREATE TABLE Response_Post_Profile(
	response_id INT FOREIGN KEY REFERENCES Response(response_id) NOT NULL,
	profile_id INT FOREIGN KEY REFERENCES Profile_User(profile_user_id) NOT NULL,
	comment_id INT FOREIGN KEY REFERENCES Comment(comment_id) NULL,
	date_publish DATETIME NOT NULL,
	PRIMARY KEY(response_id,profile_id)
);
