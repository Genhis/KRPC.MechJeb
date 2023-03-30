# Copied from https://github.com/krpc/krpc/blob/master/doc/conf.py.tmpl and modified.
project = "kRPC.MechJeb"
copyright = "2018-2023, Genhis"
author = "Genhis"

version = "0.7.0"
release = version

master_doc = "index"
source_suffix = ".rst"

extensions = ["sphinx.ext.mathjax", "sphinx.ext.todo", "sphinx.ext.extlinks", "sphinx.ext.githubpages", "sphinx_tabs.tabs", "sphinx_csharp.csharp", "javasphinx", "sphinxcontrib.luadomain"]
templates_path = ["_templates"]
language = "en"
exclude_patterns = []
pygments_style = "sphinx"

html_theme = "sphinx_rtd_theme"
html_static_path = ["_static"]

def setup(app):
    app.add_css_file("custom.css")

htmlhelp_basename = "krpc-mechjeb-doc"

add_module_names = False
sphinx_tabs_nowarn = True
toc_object_entries = False
todo_include_todos = True

javadoc_url_map = {
    "org.javatuples" : ("http://www.javatuples.org/apidocs/", "javadoc")
}
