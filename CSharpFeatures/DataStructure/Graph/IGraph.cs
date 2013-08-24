using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructure.Common;

namespace DataStructure.Graph
{
    public interface IGraph<T>
    {
        /// <summary>
        /// 顶点数组
        /// </summary>
        Node<T>[] Nodes { get; }
        /// <summary>
        /// 边的数目 
        /// </summary>
        int NumEdges { get; }
        /// <summary>
        /// 邻接矩阵数组 
        /// </summary>
        int[,] Matrix { get; }


    }

    /// <summary>
    /// 邻接矩阵无向图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphAdjMatrix<T> : IGraph<T>
    {
        #region propterties

        public Node<T>[] Nodes { get; private set; }
        public int NumEdges { get; private set; }
        public int[,] Matrix { get; private set; }
        #endregion
        /// <summary>
        /// 构造一个定点个数为n的无向图
        /// </summary>
        /// <param name="n">定点个数</param>
        public GraphAdjMatrix(int n)
        {
            Nodes = new Node<T>[n];
            Matrix = new int[n, n];
            NumEdges = 0;
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
        private void GetPoint(Node<T> v1, Node<T> v2, out int x, out int y)
        {
            //v1或v2不是图的顶点 
            if (!IsNode(v1) || !IsNode(v2))
            {
                throw new Exception("Node is not belong to Graph!");
            }
            //顶点v1与v2之间存在边 

            x = Array.IndexOf(Nodes, v1);
            y = Array.IndexOf(Nodes, v2);
            if (x == y) throw new Exception("需要建立边的两个定点不可以是同一个定点");
            //矩阵是对称矩阵,只记录上三角矩阵，
            if (x > y)
            {
                Swap(ref x, ref y);
            }
        }


    }
}
