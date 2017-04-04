# bulk-task-generation-in-tfs

**What is it?**

Just a simple utility to generate tasks in bulk in TFS. The utility contains a constructor of templates for tasks generation; the templates give abilty to specify a list of task properties that have to be set on creation. 

**What for?**

It usually took me hours to add all tasks for a sprint to TFS. The main idea is to make this procedure significantly faster. 

**How to build?**

The solution requires Team Explorer for Visual Studio 2010 (or newer) to be installed. It has references to a few Microsoft.TeamFoundation.*.dll which provide TFS API.

**How to use?**

![](Home_http://2.bp.blogspot.com/-EUIZfsFinq4/U51-RAqfVOI/AAAAAAAACE0/Q4ynLcUsJzA/s1600/Untitled2.png)

The main form is divided in 3 parts:

* **main menu:** you can select a TFS project here, specify an area and/or an iteration to filter displayed work items and select a template for tasks creation. The default template containing all required fields is created automatically for each project. You can add more templates in case of need.
* **left panel:** work items tree is displayed here; it can be filtered by area and/or iteration. You also can load a selected item specifying its ID.
* **right panel:** tasks template is displayed here; fields that should be the same for all new tasks are listed in the top part (e.g. State); items that should be unique for each item (e.g. Title) are listed in the grid below. The grid supports pasting data copied from Excel.

Use **Task Template -> Manage Templates** menu item to create/edit/delete tasks templates. 
 
![alt tag](https://raw.githubusercontent.com/marss19/bulk-task-generation-in-tfs/master/bulk-task-generation-screenshot.png)
