﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DATExplorer
{
    static class Misc
    {
        internal static TreeNode GetRootNode(TreeNode node)
        {
            if (node.Parent != null) {
                return GetRootNode(node.Parent);
            }
            return node;
        }

        // рекурсивный поиск
        internal static TreeNode FindNode(string name, TreeNode node)
        {
            foreach (TreeNode nd in node.Nodes)
            {
                if (nd.Text == name) { 
                    return nd;
                }
                TreeNode find = FindNode(name, nd);
                if (find != null) return find;
            }
            return null;
        }
 
        internal static void GetFolderFiles(OpenDat dat, List<String> listFiles, string folderPath)
        {
            TreeFiles datFolders;
            if (dat.Folders.TryGetValue(folderPath, out datFolders)) {
                foreach (var file in datFolders.GetFiles())
                {
                    listFiles.Add(file.path);
                }
            }
            // get files from sub folders
            var folders = dat.Folders.Keys;
            folderPath += '\\';
            foreach (var folder in folders)
            {
                if (folder.StartsWith(folderPath)) { 
                    datFolders = dat.Folders[folder];
                    foreach (var file in datFolders.GetFiles())
	                {
		                listFiles.Add(file.path);
	                }
                }
            } 
        }
    }
}
