import org.apache.spark.{SparkConf, SparkContext}
import org.neo4j.spark._
import org.neo4j.driver.v1._


//package main.scala

object SparkPostRecommendations {

  def main(args: Array[String]): Unit = {
    val neo4jUrl = "bolt://ec2-18-184-128-92.eu-central-1.compute.amazonaws.com:7687"
    val neo4jUn = "neo4j"
    val neo4jPass ="neo4j1"

    val conf = new SparkConf()
    conf.set("spark.neo4j.bolt.url",neo4jUrl)
    conf.set("spark.neo4j.bolt.user",neo4jUn)
    conf.set("spark.neo4j.bolt.password",neo4jPass)
    conf.setMaster("local[2]")
    conf.setAppName("SocialRecomendations")
    val sc = new SparkContext(conf)
    val neo = Neo4j(sc)
    val df = neo.cypher("match (u:User)-[fr:Following]->(f:User)-[lr:Like]->(p:Post) return u.UserId AS user,p.PostId AS post").partitions(1).batch(100).loadDataFrame
    val res = df.groupBy("user", "post").count().filter("count>=1").select("user","post") .toDF("user","post")
    val str = res.toJSON.collect.mkString("[", "," , "]" ).replace("\"user\"","user").replace("\"post\"","post")
    val query ="unwind "+str+ " as banch MATCH (u:User{UserId:banch.user}) , (p:Post{PostId:banch.post}) MERGE (p)-[r:Recomended]->(u) return r"
    val driver = GraphDatabase.driver(neo4jUrl, AuthTokens.basic(neo4jUn, neo4jPass))
    val session = driver.session
    session.run(query)
    session.close()
    driver.close()
  }
}
