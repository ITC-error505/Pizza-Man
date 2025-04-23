var RedirectPage = {
    openUrl: function(link)
    {
        window.location.href = UTF8ToString(link);
    }
};

mergeInto(LibraryManager.library, RedirectPage);