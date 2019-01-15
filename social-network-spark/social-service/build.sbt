name := "social-service"

version := "0.1"

scalaVersion := "2.12.8"

// https://mvnrepository.com/artifact/org.apache.spark/spark-core


resolvers += "Spark Packages Repo" at "http://dl.bintray.com/spark-packages/maven"

libraryDependencies += "org.apache.spark" % "spark-core_2.10" % "2.1.0"

libraryDependencies += "neo4j-contrib" % "neo4j-spark-connector" % "2.1.0-M4"