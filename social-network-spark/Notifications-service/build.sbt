name := "Notifications-service"

version := "0.1"

scalaVersion := "2.11.8"

// https://mvnrepository.com/artifact/org.apache.spark/spark-core


resolvers += "Spark Packages Repo" at "http://dl.bintray.com/spark-packages/maven"

libraryDependencies += "org.apache.spark" % "spark-core_2.11" % "2.2.0"
libraryDependencies += "org.apache.spark" % "spark-sql_2.11" % "2.2.0"
libraryDependencies += "org.apache.spark" % "spark-unsafe_2.11" % "2.2.0"
libraryDependencies += "org.apache.spark" % "spark-tags_2.11" % "2.2.0"



libraryDependencies += "neo4j-contrib" % "neo4j-spark-connector" % "2.2.1-M5"


libraryDependencies += "org.neo4j.driver" % "neo4j-java-driver" % "1.7.2"

libraryDependencies += "org.scalaj" % "scalaj-http_2.11" % "2.3.0"
