using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure.Common;

namespace DataStructure.Graph
{
    public interface IGraph<T>
    {
        /// <summary>
        /// 顶点数组
        /// </summary>
        List<Node<T>> Nodes { get; }
        /// <summary>
        /// 边的数目 
        /// </summary>
        int NumEdges { get; }
        /// <summary>
        /// 邻接矩阵数组 
        /// </summary>
        int[,] Matrix { get; }
    }

    public class GraphFactory<TData>
    {
        public static T Create<T>(Type t, int n)
        {
            return (T) Activator.CreateInstance(t, n);
        }
    }

    /// <summary>
    /// 邻接矩阵图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GraphAdjMatrixBase<T>
    {
        protected GraphAdjMatrixBase(int n)
        {
            Nodes = new List<Node<T>>(n);
            Matrix = new int[n, n];
            NumEdges = 0;
        }
        public List<Node<T>> Nodes { get; protected set; }
        public int NumEdges { get; protected set; }
        public int[,] Matrix { get; protected set; }


        /// <summary>
        /// 判断顶点v1与v2之间是否存在边
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsEdge(Node<T> v1, Node<T> v2)
        {
            int x, y;
            GetPoint(v1, v2, out x, out y);
            return Matrix[x, y] != 0;
        }

        /// <summary>
        /// 判断v是否是图的顶点 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool IsNode(Node<T> v)
        {
            //遍历顶点数组 
            return Nodes.Contains(v);
        }

        /// <summary>
        /// 添加一条V1到v2的边，其权值为v
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v"></param>
        public void SetEdge(Node<T> v1, Node<T> v2, int v)
        {
            int x, y;
            GetPoint(v1, v2, out x, out y);

            Matrix[x, y] = v;
            //Matrix[GetIndex(v2), GetIndex(v1)] = v;
            ++NumEdges;
        }

        public void SetEdge(T v1, T v2, int v)
        {
            var p1 = GetNode(v1);
            var p2 = GetNode(v2);
            SetEdge(p1, p2, v);
        }

        /// <summary>
        /// 获取边的权值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public int GetEdge(T v1, T v2)
        {
            var p1 = GetNode(v1);
            var p2 = GetNode(v2);
            return GetEdge(p1, p2);
        }
        /// <summary>
        /// 获取边的权值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public int GetEdge(Node<T> v1, Node<T> v2)
        {
            int x, y;
            GetPoint(v1, v2, out x, out y);
            return Matrix[x, y];
        }
        /// <summary>
        /// 删除顶点v1和v2之间的边 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void DelEdge(Node<T> v1, Node<T> v2)
        {
            int x, y;
            GetPoint(v1, v2, out x, out y);

            if (Matrix[x, y] != 0)
            {
                //矩阵是对称矩阵 
                Matrix[x, y] = 0;
                --NumEdges;
            }
        }
        private Node<T> GetNode(T v)
        {
            var result = Nodes.FirstOrDefault(n => n.Data.Equals(v));
            if (result == null) throw new Exception(string.Format("Node {0} not found!", v));
            return result;
        }

        /// <summary>
        /// 求入度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public virtual int GetInDegree(T v)
        {
            //矩阵第i列的不为0的边的条数
            var nodeIndex = Nodes.IndexOf(GetNode(v));
            var count = 0;
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Matrix[i, nodeIndex] != 0) count++;
            }
            return count;

        }
        /// <summary>
        /// 取出度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public virtual int GetOutDegree(T v)
        {
            //矩阵第i行的不为0的边的条数
            var nodeIndex = Nodes.IndexOf(GetNode(v));
            var count = 0;
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Matrix[nodeIndex, i] != 0) count++;
            }
            return count;

        }
        /// <summary>
        /// 获取定点坐标
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected abstract void GetPoint(Node<T> v1, Node<T> v2, out int x, out int y);

        /// <summary>
        /// 输出矩阵
        /// </summary>
        public void Print()
        {
            for (var i = 0; i <= Nodes.Count; i++)
            {
                if (i == 0)
                {
                    PrintTittle();
                    continue;
                }
                for (var j = 0; j <= Nodes.Count; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(Nodes[i - 1].Data + "\t");
                        continue;
                    }

                    Console.Write(Matrix[i - 1, j - 1] + "\t");
                    if (j == Nodes.Count) Console.Write("\n");
                }
            }
        }

        private void PrintTittle()
        {
            var tittle = new StringBuilder("\t", Nodes.Count * 4);
            Nodes.ForEach(node => tittle.Append(node.Data.ToString() + "\t"));
            //string title = 
            Console.WriteLine(tittle);
        }
    }

    /// <summary>
    /// 邻接矩阵无向图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphAdjMatrix<T> : GraphAdjMatrixBase<T>, IGraph<T>
    {
        /// <summary>
        /// 构造一个定点个数为n的无向图
        /// </summary>
        /// <param name="n">定点个数</param>
        public GraphAdjMatrix(int n)
            : base(n)
        {

        }

        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Swap(ref int x, ref int y)
        {
            x += y;
            y = x - y;
            x -= y;
        }

        /// <summary>
        /// 获取定点坐标
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected override void GetPoint(Node<T> v1, Node<T> v2, out int x, out int y)
        {
            //v1或v2不是图的顶点 
            if (!IsNode(v1) || !IsNode(v2))
            {
                throw new Exception("Node is not belong to Graph!");
            }
            //顶点v1与v2之间存在边 

            x = Nodes.IndexOf(v1);
            y = Nodes.IndexOf(v2);
            if (x == y) throw new Exception("需要建立边的两个定点不可以是同一个定点");
            //矩阵是对称矩阵,只记录上三角矩阵，
            if (x > y)
            {
                Swap(ref x, ref y);
            }
        }

        public override int GetInDegree(T v)
        {
            return GetDegree(v);
        }

        public override int GetOutDegree(T v)
        {
            return GetDegree(v);
        }

        private int GetDegree(T v)
        {
            return base.GetInDegree(v) + base.GetOutDegree(v);
        }

    }
    /// <summary>
    /// 邻接矩阵有向图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DirectedGraphAdjMatrix<T> : GraphAdjMatrixBase<T>
    {
        /// <summary>
        /// 构造一个定点个数为n的无向图
        /// </summary>
        /// <param name="n">定点个数</param>
        public DirectedGraphAdjMatrix(int n)
            : base(n) { }

        /// <summary>
        /// 获取定点坐标
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected override void GetPoint(Node<T> v1, Node<T> v2, out int x, out int y)
        {
            //v1或v2不是图的顶点 
            if (!IsNode(v1) || !IsNode(v2))
            {
                throw new Exception("Node is not belong to Graph!");
            }
            //顶点v1与v2之间存在边 
            x = Nodes.IndexOf(v1);
            y = Nodes.IndexOf(v2);
            if (x == y) throw new Exception("需要建立边的两个定点不可以是同一个定点");

        }
    }
}
