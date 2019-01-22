import org.apache.spark.{SparkConf, SparkContext}
import org.neo4j.spark._
import scalaj.http.{Http, HttpOptions}

object sparkNotifications {

  def main(args: Array[String]): Unit = {
    val neo4jUrl = "bolt://ec2-18-184-128-92.eu-central-1.compute.amazonaws.com:7687"
    val neo4jUn = "neo4j"
    val neo4jPass ="neo4j1"

    val conf = new SparkConf()
    conf.set("spark.neo4j.bolt.url",neo4jUrl)
    conf.set("spark.neo4j.bolt.user",neo4jUn)
    conf.set("spark.neo4j.bolt.password",neo4jPass)
    conf.setMaster("local[2]")
    conf.setAppName("SocialNotifications")
    val sc = new SparkContext(conf)
    val neo = Neo4j(sc)
    val df = neo.cypher("MATCH(u:User)-[:Following]->(:User)-[f:Following]->(u2:User) WHERE u2.UserId <> u.UserId AND NOT EXISTS((u)-[:Blocked]-(u2)) AND NOT EXISTS((u)-[:Following]-(u2)) RETURN u.UserId AS UserId,u2.UserId AS RecommededUserId").partitions(1).batch(100).loadDataFrame
    val res = df.groupBy("UserId", "RecommededUserId").count().filter("count>=2").select("UserId","RecommededUserId") .toDF("UserId","RecommededUserId")
    var str = res.toJSON.collect.mkString("[", "," , "]" )
    println(str)
    val ServerUrl ="http://SocialNotifications.com/api/Notification/RecommendFollwers"
    var LocalUrl = "http://localhost:65291/api/Notification/RecommendFollwers"
    val serverToken ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTb2NpYWxOb3RpZmljYXRpb25TcGFyayIsImlhdCI6MTUxNjIzOTAyMiwiZXhwIjoyMTQ3MDAwMDAwLCJhdWQiOiJzb2NpYWwgbmV0d29yayIsIklzU2VydmVyIjoiVHJ1ZSJ9.WcGm_5jrZF7UoF0XhYY020iL3uAprreX7x9AFat_GEU"
    val httpResult = Http(LocalUrl).postData(str)
      .header("Content-Type", "application/json")
      .header("Charset", "UTF-8")
      .header("x-auth-token",serverToken)
      .option(HttpOptions.readTimeout(10000)).asString
    println(httpResult);


  }

}
